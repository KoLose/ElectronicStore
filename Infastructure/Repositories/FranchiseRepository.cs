using Domain.Models;
using Infastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class FranchiseRepository
{
    private readonly ContextDb _context;

    public FranchiseRepository(ContextDb context) => _context = context;

    public async Task CreateAsync(Franchise franchise)
    {
        await _context.Franchises.AddAsync(franchise);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Order>> GetOrdersByFranchiseIdAsync(int franchiseId) =>
        await _context.Orders
            .Include(o => o.OrderProducts)
            .Where(o => o.OrderProducts.Any(op =>
                _context.FranchiseProducts.Any(fp => fp.FranchiseId == franchiseId && fp.ProductId == op.ProductId)))
            .ToListAsync();
}