using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Entities;

namespace TokonyadiaRestAPI.Services;

public interface IPurchaseService
{
    Task<PurchaseResponse> GetById(string id);
    Task<PurchaseResponse> CreateNewPurchase(Purchase payload);
}