namespace Domain.Models;

public class User
{
    private const int MaxLoginLength = 20;
    private const int MaxPasswordLength = 20;
    private User(string login, string passwordHash)
    {
        Login = login;
        PasswordHash = passwordHash;
    }

    public int Id { get; private set; }
    
    public int RoleId { get; private set; }
    public Role? Role { get; private set; }
    
    public string Login { get; private set; }
    public string PasswordHash { get; private set; }
    
    public static (User? user, string error) Create(string login, string passwordHash)
    {
        if (string.IsNullOrEmpty(login) || login.Length > MaxLoginLength)
            return (null, "Login cannot be empty or exceed 20 characters");
        
        else if (string.IsNullOrEmpty(passwordHash) || passwordHash.Length > MaxPasswordLength)
            return (null, "Password cannot be empty or exceed 20 characters");
        
        return (new User(login, passwordHash), string.Empty);
    }
}