using Domain.Models;
using Infastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class ReviewRepository
{
    private readonly ContextDb _context;

    public ReviewRepository(ContextDb context) => _context = context;

    public async Task CreateAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Review>> GetByProductIdAsync(int productId) =>
        await _context.Reviews.Where(r => r.ProductId == productId).ToListAsync();
}