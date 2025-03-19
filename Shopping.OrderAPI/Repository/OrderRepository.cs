using Microsoft.EntityFrameworkCore;
using Shopping.OrderAPI.Model;
using Shopping.OrderAPI.Model.Context;

namespace Shopping.OrderAPI.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly DbContextOptions<Context> _context;

    public OrderRepository(DbContextOptions<Context> context)
    {
        _context = context;
    }

    public async Task<bool> AddOrderAsync(OrderHeader header)
    {
        if (header == null)
            return false;
        
        await using var _db = new Context(_context);
        _db.Headers.Add(header);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task UpdateOrderPaymentStatusAsync(long orderHeaderId, bool paid)
    {
        await using var _db = new Context(_context);
        var header = await _db.Headers.FirstOrDefaultAsync(x => x.Id == orderHeaderId);

        if (header != null)
        {
            header.PaymentStatus = paid;
            await _db.SaveChangesAsync();
        }
    }
}