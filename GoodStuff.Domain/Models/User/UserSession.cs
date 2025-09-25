namespace GoodStuff.Domain.Models.User;

public class UserSession
{
    public UserModel UserData { get; set; }
    public List<string> Roles { get; set; } = new();
    public DateTime LoginTime { get; set; }
    public DateTime LastActivity { get; set; }
    public string IpAddress { get; set; }
}