namespace GoodStuff.Website.Domain.Entities.User;

public class UserSession
{
    public User UserData { get; set; }
    public List<string> Roles { get; set; } = new();
    public DateTime LoginTime { get; set; }
    public DateTime LastActivity { get; set; }
    public string IpAddress { get; set; }
}