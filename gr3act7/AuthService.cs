using System.Collections.Generic;
using System.Linq;

public class AuthService
{
    private List<User> users = new();
    public User? CurrentUser { get; private set; }

    public bool IsLoggedIn => CurrentUser != null;

    public bool Register(string username, string password)
    {
        if (users.Any(u => u.Username == username)) return false;
        users.Add(new User { Username = username, Password = password });
        return true;
    }

    public bool Login(string username, string password)
    {
        var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            CurrentUser = user;
            return true;
        }
        return false;
    }

    public void Logout()
    {
        CurrentUser = null;
    }
}