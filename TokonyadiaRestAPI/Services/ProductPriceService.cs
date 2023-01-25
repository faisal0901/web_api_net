using TokonyadiaEF.Entities;
using TokonyadiaRestAPI.Entities;
using TokonyadiaRestAPI.Exception;
using TokonyadiaRestAPI.Exceptions;
using TokonyadiaRestAPI.Repositories;

namespace TokonyadiaRestAPI.Services;

public class ProductPriceService:IProductPriceService
{
    private readonly IRepository<ProductPrice> _repository;

    public ProductPriceService(IRepository<ProductPrice> repository)
    {
        _repository = repository;
    }

    public async Task<ProductPrice> GetById(string id)
    {
        try
        {
            var productPrice = await _repository.Find(price => price.Id.Equals(Guid.Parse(id)), new[] { "Product" });
            if (productPrice is null) throw new NotFoundException("product price not found");
            return productPrice;
        }
        catch (NotFoundException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}