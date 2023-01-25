using TokonyadiaEF.Entities;

namespace TokonyadiaRestAPI.Services;

public interface IProductPriceService
{
    Task<ProductPrice> GetById(string id);
}