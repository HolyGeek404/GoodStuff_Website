using Website.Models.User;

namespace Website.Services.Interfaces;

public interface IUserSessionService
{
    string CreateSession(UserModel userModel);
    UserSession? GetUserSession();
    bool Validate();
    void ClearUserCachedData(string sessionId);
}