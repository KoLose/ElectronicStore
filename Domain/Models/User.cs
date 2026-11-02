namespace Domain.Models;

public class User
{
    private const int MaxLoginLength = 20;
    private User(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public int Id { get; private set; }
    public string Login { get; private set; }
    public string Password { get; private set; }
    
    public static (User? user, string error) Create(string login, string password)
    {
        if (string.IsNullOrEmpty(login) || login.Length > MaxLoginLength)
            return (null, "Login cannot be empty or exceed 20 characters");

        return (new User(login, password), string.Empty);
    }
}