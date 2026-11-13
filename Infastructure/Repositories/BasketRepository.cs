using Domain.Models;
using Infastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class BasketRepository
{
    private readonly ContextDb _context;

    public BasketRepository(ContextDb context) => _context = context;

    public async Task<List<Basket>> GetByUserIdAsync(int userId) =>
        await _context.Baskets.Where(b => b.UserId == userId).ToListAsync();

    public async Task<Basket?> GetByUserIdAndProductIdAsync(int userId, int productId) =>
        await _context.Baskets.FirstOrDefaultAsync(b => b.UserId == userId && b.ProductId == productId);

    public async Task AddAsync(Basket basket)
    {
        await _context.Baskets.AddAsync(basket);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Basket basket)
    {
        _context.Baskets.Update(basket);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int userId, int productId)
    {
        var item = await _context.Baskets.FirstOrDefaultAsync(b => b.UserId == userId && b.ProductId == productId);
        if (item != null)
        {
            _context.Baskets.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task ClearByUserIdAsync(int userId)
    {
        var items = await _context.Baskets.Where(b => b.UserId == userId).ToListAsync();
        _context.Baskets.RemoveRange(items);
        await _context.SaveChangesAsync();
    }
}