using GoodStuff.Website.Domain.Models.User;

namespace GoodStuff.Website.Application.Services.Interfaces;

public interface IUserSessionService
{
    string CreateSession(UserModel userModel);
    UserSession? GetUserSession();
    bool Validate();
    void ClearUserCachedData(string sessionId);
}