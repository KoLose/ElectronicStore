using Domain.Models;
using Infastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class OrderRepository
{
    private readonly ContextDb _context;

    public OrderRepository(ContextDb context) => _context = context;

    public async Task CreateAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Order>> GetByUserIdAsync(int userId) =>
        await _context.Orders.Include(o => o.OrderProducts).Where(o => o.UserId == userId).ToListAsync();

    public async Task<Order?> GetByIdAsync(int id) =>
        await _context.Orders.Include(o => o.OrderProducts).FirstOrDefaultAsync(o => o.Id == id);

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}