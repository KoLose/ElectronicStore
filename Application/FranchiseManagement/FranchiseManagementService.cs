using Domain.Models;
using Infastructure.Repositories;

namespace Application.FranchiseManagement;

public class FranchiseManagementService
{
    private readonly FranchiseRepository _franchiseRepo;
    private readonly FranchiseClientRepository _franchiseClientRepo;
    private readonly FranchiseProductRepository _franchiseProductRepo;

    public FranchiseManagementService(FranchiseRepository franchiseRepo,
                                      FranchiseClientRepository franchiseClientRepo,
                                      FranchiseProductRepository franchiseProductRepo)
    {
        _franchiseRepo = franchiseRepo;
        _franchiseClientRepo = franchiseClientRepo;
        _franchiseProductRepo = franchiseProductRepo;
    }

    public async Task<Franchise> CreateFranchiseAsync(string name, string address, string phone)
    {
        var result = Franchise.Create(name, address, phone);
        if (result.error is not null) throw new Exception(result.error);

        await _franchiseRepo.CreateAsync(result.franchise!);
        return result.franchise!;
    }

    public async Task AssignEmployeeAsync(int franchiseId, int userId)
    {
        var result = FranchiseClient.Create(franchiseId, userId);
        if (result.error is not null) throw new Exception(result.error);

        await _franchiseClientRepo.AddAsync(result.franchiseClient!);
    }

    public async Task RemoveEmployeeAsync(int franchiseId, int userId) =>
        await _franchiseClientRepo.RemoveAsync(franchiseId, userId);

    public async Task UpdateStockAsync(int franchiseProductId, int quantity)
    {
        var existing = await _franchiseProductRepo.GetByIdAsync(franchiseProductId);
        if (existing == null) throw new Exception("Franchise product not found");

        var result = FranchiseProduct.Create(existing.FranchiseId, existing.ProductId, existing.Price, quantity);
        if (result.error is not null) throw new Exception(result.error);

        await _franchiseProductRepo.UpdateStockAsync(result.franchiseProduct!);
    }

    public async Task<List<Order>> GetFranchiseOrdersAsync(int franchiseId) =>
        await _franchiseRepo.GetOrdersByFranchiseIdAsync(franchiseId);
}