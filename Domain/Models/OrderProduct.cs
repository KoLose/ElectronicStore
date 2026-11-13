namespace Domain.Models;

public class OrderProduct
{
    private OrderProduct(int orderId, int productId, int quantity, decimal unitPrice)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public int Id { get; private set; }
    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalPrice => Quantity * UnitPrice;

    public static (OrderProduct? orderProduct, string error) Create(int orderId, int productId, int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
            return (null, "Invalid quantity");

        if (unitPrice <= 0)
            return (null, "Price must be positive");

        return (new OrderProduct(orderId, productId, quantity, unitPrice), string.Empty);
    }
}