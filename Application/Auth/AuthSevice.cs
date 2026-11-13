using Domain.Models;
using Infastructure.Repositories;

namespace Application.Auth;

public class AuthService
{
    private readonly UserRepository _userRepo;

    public AuthService(UserRepository userRepo) => _userRepo = userRepo;

    public async Task<User> RegisterAsync(string login, string password)
    {
        var existing = await _userRepo.GetByLoginAsync(login);
        if (existing != null) throw new Exception("Login already exists");

        var result = User.Create(login, password);
        if (result.error is not null) throw new Exception(result.error);

        await _userRepo.CreateAsync(result.user!);
        return result.user!;
    }

    public async Task<string> LoginAsync(string login, string password)
    {
        var user = await _userRepo.GetByLoginAsync(login);
        if (user == null || !VerifyPassword(password, user.PasswordHash)) 
            throw new Exception("Invalid credentials");

        return GenerateToken(user);
    }

    private string HashPassword(string password) => password;
    private bool VerifyPassword(string password, string hash) => password == hash;
    private string GenerateToken(User user) => $"token_{user.Id}";
}