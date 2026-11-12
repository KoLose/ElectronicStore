namespace Domain.Models;

public class Basket
{
    private Basket(int userId, int productId, int quantity)
    {
        UserId = userId;
        ProductId = productId;
        Quantity = quantity;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }

    public static (Basket? basket, string error) Create(int userId, int productId, int quantity)
    {
        if (quantity <= 0)
            return (null, "Quantity must be positive");

        return (new Basket(userId, productId, quantity), string.Empty);
    }
}