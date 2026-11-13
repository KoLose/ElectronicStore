namespace Domain.Models;

public class Order
{
    private Order(int userId, decimal totalPrice)
    {
        UserId = userId;
        TotalPrice = totalPrice;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
    public OrderStatus Status { get; private set; } = OrderStatus.New;
    public decimal TotalPrice { get; private set; }

    public static (Order? order, string error) Create(int userId, decimal totalPrice)
    {
        if (totalPrice <= 0)
            return (null, "Total price must be positive");

        return (new Order(userId, totalPrice), string.Empty);
    }
}

public enum OrderStatus
{
    New = 1,
    Processing = 2,
    Shipped = 3,
    Delivered = 4,
    Cancelled = 5
}