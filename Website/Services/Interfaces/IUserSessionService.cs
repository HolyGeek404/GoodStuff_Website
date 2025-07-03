using Website.Models.User;

namespace Website.Services.Interfaces;

public interface IUserSessionService
{
    Task<bool> CreateSession(UserModel userModel);
    Task<UserSession?> GetUserSessionAsync();
}
