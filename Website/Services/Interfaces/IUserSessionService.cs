using Website.Models.User;

namespace Website.Services.Interfaces;

public interface IUserSessionService
{
    bool CreateSession(UserModel userModel);
    UserSession? GetUserSession();
    bool Validate();
}
