using Domain.Models;
using Infastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class FranchiseProductRepository
{
    private readonly ContextDb _context;

    public FranchiseProductRepository(ContextDb context) => _context = context;

    public async Task<FranchiseProduct?> GetByIdAsync(int id) =>
        await _context.FranchiseProducts.FindAsync(id);

    public async Task<FranchiseProduct?> GetByProductIdAsync(int productId) =>
        await _context.FranchiseProducts.FirstOrDefaultAsync(x => x.ProductId == productId);

    public async Task UpdateStockAsync(FranchiseProduct franchiseProduct)
    {
        _context.FranchiseProducts.Update(franchiseProduct);
        await _context.SaveChangesAsync();
    }
}