using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Entities;

namespace TokonyadiaRestAPI.Services;

public interface IPurchaseService
{
    Task<PurchaseResponse> CreateNewProduct(Purchase payload);
    Task<PageResponse<PurchaseResponse>> GetAll(string? name,int page,int size);
}