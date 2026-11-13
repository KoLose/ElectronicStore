using Domain.Models;
using Infastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class ProductRepository
{
    private readonly ContextDb _context;

    public ProductRepository(ContextDb context) => _context = context;

    public async Task<List<Product>> GetAllAsync(int? categoryId)
    {
        var query = _context.Products.AsQueryable();
        if (categoryId.HasValue) query = query.Where(p => p.CategoryId == categoryId.Value);
        return await query.ToListAsync();
    }

    public async Task<List<Product>> SearchAsync(string query) =>
        await _context.Products.Where(p => p.Name.Contains(query)).ToListAsync();

    public async Task<Product?> GetByIdAsync(int id) =>
        await _context.Products.FindAsync(id);
}