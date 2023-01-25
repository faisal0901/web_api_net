using TokonyadiaEF.Entities;

namespace TokonyadiaRestAPI.Services;

public interface IStoreService
{
     Task<StoreResponse> CreateNewStore(Store store);
     Task<IEnumerable<Store>> GetAllStore();
     Task<StoreResponse> GetStoreById(string id);
     Task<StoreResponse> UpdateStore(Store store);
     Task DeleteStoreById(string id);
}