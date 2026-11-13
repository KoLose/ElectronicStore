namespace Domain.Models;

public class Category
{
    private const int MaxNameLength = 100;

    private Category(string name, int? parentCategoryId)
    {
        Name = name;
        ParentCategoryId = parentCategoryId;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public int? ParentCategoryId { get; private set; }

    public static (Category? category, string error) Create(string name, int? parentCategoryId = null)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
            return (null, "Invalid category name");

        return (new Category(name, parentCategoryId), string.Empty);
    }
}