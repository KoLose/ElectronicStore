using Domain.Models;
using Infastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Repositories;

public class PaymentRepository
{
    private readonly ContextDb _context;

    public PaymentRepository(ContextDb context) => _context = context;

    public async Task CreateAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Payment>> GetByOrderIdAsync(int orderId) =>
        await _context.Payments.Where(p => p.OrderId == orderId).ToListAsync();
}