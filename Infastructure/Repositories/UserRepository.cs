using Domain.Models;
using Infastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class UserRepository
{
    private readonly ContextDb _context;

    public UserRepository(ContextDb context) => _context = context;

    public async Task<User?> GetByLoginAsync(string login) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Login == login);

    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}