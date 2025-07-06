using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Memory;
using Website.Models.User;
using Website.Services.Interfaces;

namespace Website.Services;

public class UserSessionService(IMemoryCache cache,
                                IHttpContextAccessor httpContextAccessor,
                                ILogger<UserSessionService> logger) : IUserSessionService
{
    private const int SessionTimeoutMinutes = 30;

    public bool CreateSession(UserModel userModel)
    {
        logger.LogInformation($"Creating session for user {userModel.Email}.");

        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null) return false;

        try
        {
            var sessionId = GenerateSecureSessionId();
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(SessionTimeoutMinutes),
                SlidingExpiration = TimeSpan.FromMinutes(15),
                Priority = CacheItemPriority.High
            };
            var userSession = new UserSession
            {
                UserData = userModel,
                LastActivity = DateTime.UtcNow,
                LoginTime = DateTime.UtcNow,
                IpAddress = GetClientIpAddress()
            };

            cache.Set(GetCacheKey(sessionId), userSession, cacheOptions);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                MaxAge = TimeSpan.FromMinutes(SessionTimeoutMinutes)
            };

            httpContext.Response.Cookies.Append("UserSessionId", sessionId, cookieOptions);
            logger.LogInformation($"Created session for user {userModel.Email}.");

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error creating user session for {userModel.Email}");
            throw;
        }
    }
    public UserSession? GetUserSession()
    {
        try
        {
            var sessionId = GetSessionIdFromCookie();
            if (sessionId == null) return null;

            var cachedKey = GetCacheKey(sessionId);
            if (cache.TryGetValue(cachedKey, out UserSession? userSession))
            {
                userSession!.LastActivity = DateTime.UtcNow;
            }
            
            return userSession;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error during getting session");
            throw;
        }
    }
    public bool Validate()
    {
        try
        {
            var session = GetUserSession();
            if (session == null) return false;

            var sessionAge = DateTime.UtcNow - session.LastActivity;
            if (sessionAge.TotalMinutes > SessionTimeoutMinutes)
            {
                ClearUserSession();
                return false;
            }

            var currentIp = GetClientIpAddress();
            if (currentIp != session.IpAddress)
            {
                ClearUserSession();
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError($"Couldn't validate session because: {ex}");
            throw;
        }
    }

    public void SignOut()
    {
        try
        {
            ClearUserSession();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Couldn't sign out user because: {ex}");
            throw;
        }
    }


    #region Private
    private void ClearUserSession()
    {
        var sessionId = GetSessionIdFromCookie();
        if (!string.IsNullOrEmpty(sessionId))
        {
            cache.Remove(sessionId);
        }

        logger.LogInformation("User session cleared");
    }

    private string? GetSessionIdFromCookie()
    {
        return httpContextAccessor.HttpContext?.Request.Cookies["UserSessionId"];
    }
    private string GetCacheKey(string sessionId)
    {
        return $"user_session_{sessionId}";
    }
    private string GenerateSecureSessionId()
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[32];
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
    }
    private string GetClientIpAddress()
    {
        if (httpContextAccessor == null) return "unknown";

        var forwardedFor = httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedFor))
        {
            return forwardedFor.Split(',')[0].Trim();
        }

        return httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }
    #endregion
}