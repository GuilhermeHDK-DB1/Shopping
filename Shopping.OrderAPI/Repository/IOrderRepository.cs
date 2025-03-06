using Shopping.OrderAPI.Model;

namespace Shopping.OrderAPI.Repository;

public interface IOrderRepository
{
    Task<bool> AddOrderAsync(OrderHeader header);
    Task UpdateOrderPaymentStatusAsync(long orderHeaderId, bool paid);
}