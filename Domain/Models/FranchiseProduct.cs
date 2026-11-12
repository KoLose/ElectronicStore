namespace Domain.Models;

public class FranchiseProduct
{
    private FranchiseProduct(int franchiseId, int productId, decimal price, int stockQuantity)
    {
        FranchiseId = franchiseId;
        ProductId = productId;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public int Id { get; private set; }
    public int FranchiseId { get; private set; }
    public int ProductId { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }

    public static (FranchiseProduct? franchiseProduct, string error) Create(int franchiseId, int productId, decimal price, int stockQuantity)
    {
        if (price <= 0)
            return (null, "Price must be positive");

        if (stockQuantity < 0)
            return (null, "Invalid stock quantity");

        return (new FranchiseProduct(franchiseId, productId, price, stockQuantity), string.Empty);
    }
}