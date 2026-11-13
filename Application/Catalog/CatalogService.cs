using Domain.Models;
using Infastructure.Repositories;

namespace Application.Catalog;

public class CatalogService
{
    private readonly ProductRepository _productRepo;
    private readonly ReviewRepository _reviewRepo;

    public CatalogService(ProductRepository productRepo, ReviewRepository reviewRepo)
    {
        _productRepo = productRepo;
        _reviewRepo = reviewRepo;
    }

    public async Task<List<Product>> GetProductsAsync(int? categoryId = null) =>
        await _productRepo.GetAllAsync(categoryId);

    public async Task<List<Product>> SearchAsync(string query) =>
        await _productRepo.SearchAsync(query);

    public async Task<Product?> GetProductDetailsAsync(int productId)
    {
        var product = await _productRepo.GetByIdAsync(productId);
        if (product == null) return null;
        
        product.Reviews = await _reviewRepo.GetByProductIdAsync(productId);
        return product;
    }
}