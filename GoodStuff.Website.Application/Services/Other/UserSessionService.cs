using System.Security.Cryptography;
using GoodStuff.Website.Application.Services.Interfaces;
using GoodStuff.Website.Domain.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace GoodStuff.Website.Application.Services.Other;

public class UserSessionService(
    IMemoryCache cache,
    IHttpContextAccessor? httpContextAccessor,
    ILogger<UserSessionService> logger) : IUserSessionService
{
    private const int SessionTimeoutMinutes = 30;

    public string CreateSession(User user)
    {
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
                UserData = user,
                LastActivity = DateTime.UtcNow,
                LoginTime = DateTime.UtcNow,
                IpAddress = GetClientIpAddress()
            };

            cache.Set(GetCacheKey(sessionId), userSession, cacheOptions);
            return sessionId;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error creating user session for {user.Email}");
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
            if (cache.TryGetValue(cachedKey, out UserSession? userSession)) userSession!.LastActivity = DateTime.UtcNow;

            return userSession;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during getting session");
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
                var sessionId = GetSessionIdFromCookie();
                if (sessionId != null) ClearUserCachedData(sessionId);
                return false;
            }

            var currentIp = GetClientIpAddress();
            if (currentIp == session.IpAddress) return true;
            {
                var sessionId = GetSessionIdFromCookie();
                if (sessionId != null) ClearUserCachedData(sessionId);
                return false;
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Couldn't validate session because: {ex}");
            throw;
        }
    }

    public void ClearUserCachedData(string sessionId)
    {
        if (!string.IsNullOrEmpty(sessionId)) cache.Remove(GetCacheKey(sessionId));
        logger.LogInformation("User session cleared");
    }

    #region Private

    private string? GetSessionIdFromCookie()
    {
        return httpContextAccessor?.HttpContext?.Request.Cookies["UserSessionId"];
    }

    private static string GetCacheKey(string sessionId)
    {
        return $"user_session_{sessionId}";
    }

    private static string GenerateSecureSessionId()
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
        if (!string.IsNullOrEmpty(forwardedFor)) return forwardedFor.Split(',')[0].Trim();

        return httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }

    #endregion
}