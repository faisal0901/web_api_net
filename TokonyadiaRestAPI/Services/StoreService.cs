using TokonyadiaEF.Entities;
using TokonyadiaRestAPI.Repositories;

namespace TokonyadiaRestAPI.Services;

public class StoreService:IStoreService
{
    private readonly IRepository<Store> _storeRepository;
    private readonly IPersistence _persistence;
    private readonly ILogger _logger;

    public StoreService(IRepository<Store> storeRepository,IPersistence persistence)
    {
        _storeRepository = storeRepository;
        _persistence = persistence;

    }
    public async Task<StoreResponse> CreateNewStore(Store store)
    {
        var entry = await _storeRepository.Save(store);
        await _persistence.SaveChangesAsync();
        StoreResponse storeResponse = new()
        {
            Id = entry.Id.ToString(),
            PhoneNumber = entry.PhoneNumber,
            SiupNumber = entry.SiupNumber,
            StoreName = entry.StoreName
        };
        return storeResponse;
    }

    public async Task<IEnumerable<Store>> GetAllStore()
    {
        var entry = await _storeRepository.FindAll();
        return entry;
    }

    public Task<StoreResponse> GetStoreById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<StoreResponse> UpdateStore(Store store)
    {
        throw new NotImplementedException();
    }

    public Task DeleteStoreById(string id)
    {
        throw new NotImplementedException();
    }
}