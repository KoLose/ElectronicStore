using Domain.Models;
using Infastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class FranchiseClientRepository
{
    private readonly ContextDb _context;

    public FranchiseClientRepository(ContextDb context) => _context = context;

    public async Task AddAsync(FranchiseClient client)
    {
        await _context.FranchiseClients.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int franchiseId, int userId)
    {
        var client = await _context.FranchiseClients.FirstOrDefaultAsync(fc => fc.FranchiseId == franchiseId && fc.UserId == userId);
        if (client != null)
        {
            _context.FranchiseClients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}