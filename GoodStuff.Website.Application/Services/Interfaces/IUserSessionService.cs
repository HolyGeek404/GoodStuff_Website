using GoodStuff.Website.Domain.Entities.User;

namespace GoodStuff.Website.Application.Services.Interfaces;

public interface IUserSessionService
{
    string CreateSession(User user);
    UserSession? GetUserSession();
    bool Validate();
    void ClearUserCachedData(string sessionId);
}