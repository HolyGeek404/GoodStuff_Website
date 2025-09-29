using GoodStuff.Domain.Models.User;

namespace GoodStuff.Core.Services.Interfaces;

public interface IUserSessionService
{
    string CreateSession(UserModel userModel);
    UserSession? GetUserSession();
    bool Validate();
    void ClearUserCachedData(string sessionId);
}