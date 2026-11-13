using Domain.Models;
using Infastructure.Repositories;

namespace Application.Checkout;

public class CheckoutService
{
    private readonly BasketRepository _basketRepo;
    private readonly OrderRepository _orderRepo;
    private readonly PaymentRepository _paymentRepo;
    private readonly FranchiseProductRepository _franchiseProductRepo;

    public CheckoutService(BasketRepository basketRepo, 
                           OrderRepository orderRepo, 
                           PaymentRepository paymentRepo,
                           FranchiseProductRepository franchiseProductRepo)
    {
        _basketRepo = basketRepo;
        _orderRepo = orderRepo;
        _paymentRepo = paymentRepo;
        _franchiseProductRepo = franchiseProductRepo;
    }

    public async Task<List<Basket>> GetCartAsync(int userId) =>
        await _basketRepo.GetByUserIdAsync(userId);

    public async Task AddToCartAsync(int userId, int productId, int quantity)
    {
        var existing = await _basketRepo.GetByUserIdAndProductIdAsync(userId, productId);
        if (existing != null)
        {
            var result = Basket.Create(userId, productId, existing.Quantity + quantity);
            if (result.error is not null) throw new Exception(result.error);
            
            await _basketRepo.UpdateAsync(result.basket!);
        }
        else
        {
            var result = Basket.Create(userId, productId, quantity);
            if (result.error is not null) throw new Exception(result.error);
            await _basketRepo.AddAsync(result.basket!);
        }
    }

    public async Task RemoveFromCartAsync(int userId, int productId) =>
        await _basketRepo.RemoveAsync(userId, productId);

    public async Task<Order> CreateOrderAsync(int userId)
    {
        var cart = await _basketRepo.GetByUserIdAsync(userId);
        if (!cart.Any()) throw new Exception("Cart is empty");

        decimal totalPrice = 0;
        var orderProducts = new List<OrderProduct>();

        foreach (var item in cart)
        {
            var fp = await _franchiseProductRepo.GetByProductIdAsync(item.ProductId);
            if (fp == null) throw new Exception($"Product {item.ProductId} unavailable");
            if (fp.StockQuantity < item.Quantity) throw new Exception($"Insufficient stock for {item.ProductId}");

            var opResult = OrderProduct.Create(0, item.ProductId, item.Quantity, fp.Price);
            if (opResult.error is not null) throw new Exception(opResult.error);

            orderProducts.Add(opResult.orderProduct!);
            totalPrice += fp.Price * item.Quantity;
        }

        var orderResult = Order.Create(userId, totalPrice);
        if (orderResult.error is not null) throw new Exception(orderResult.error);

        orderResult.order!.OrderProducts = orderProducts;

        await _orderRepo.CreateAsync(orderResult.order!);
        await _basketRepo.ClearByUserIdAsync(userId);
        return orderResult.order!;
    }

    public async Task<Payment> PayOrderAsync(int orderId, int bankId, decimal amount)
    {
        var order = await _orderRepo.GetByIdAsync(orderId);
        if (order == null) throw new Exception("Order not found");
        
        if ((int)order.Status != 1) throw new Exception("Order already processed");

        var paymentResult = Payment.Create(orderId, bankId, amount);
        if (paymentResult.error is not null) throw new Exception(paymentResult.error);

        await _paymentRepo.CreateAsync(paymentResult.payment!);
        
        var updatedOrder = Order.Create(order.UserId, order.TotalPrice);
        if (updatedOrder.error is not null) throw new Exception(updatedOrder.error);
        
        await _orderRepo.UpdateAsync(updatedOrder.order!);

        return paymentResult.payment!;
    }

    public async Task<List<Order>> GetUserOrdersAsync(int userId) =>
        await _orderRepo.GetByUserIdAsync(userId);

    public async Task CancelOrderAsync(int orderId)
    {
        var order = await _orderRepo.GetByIdAsync(orderId);
        if (order == null) throw new Exception("Order not found");
        if ((int)order.Status != 1) throw new Exception("Only pending orders can be cancelled");
        
        var updatedOrder = Order.Create(order.UserId, order.TotalPrice);
        if (updatedOrder.error is not null) throw new Exception(updatedOrder.error);
        
        await _orderRepo.UpdateAsync(updatedOrder.order!);
    }
}