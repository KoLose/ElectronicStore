namespace Domain.Models;

public class Product
{
    private const int MaxNameLength = 200;
    private const int MaxSkuLength = 50;

    private Product(string name, string? description, string sku, int categoryId, decimal basePrice)
    {
        Name = name;
        Description = description;
        Sku = sku;
        CategoryId = categoryId;
        BasePrice = basePrice;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string Sku { get; private set; }
    public int CategoryId { get; private set; }
    public decimal BasePrice { get; private set; }
    public List<Review> Reviews { get; set; }

    public static (Product? product, string error) Create(
        string name, string sku, int categoryId, decimal basePrice, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
            return (null, "Invalid product name");

        if (string.IsNullOrWhiteSpace(sku) || sku.Length > MaxSkuLength)
            return (null, "Invalid SKU");

        if (basePrice <= 0)
            return (null, "Price must be positive");

        return (new Product(name, description, sku, categoryId, basePrice), string.Empty);
    }
}