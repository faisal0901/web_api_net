using Microsoft.EntityFrameworkCore;
using TokonyadiaEF.Entities;
using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Repositories;

namespace TokonyadiaRestAPI.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IPersistence _persistence;

    public ProductService(IRepository<Product> productRepository, IPersistence persistence)
    {
        _productRepository = productRepository;
        _persistence = persistence;
    }

    public async Task<ProductResponse> CreateNewProduct(Product payload)
    {
        var product = await _productRepository.Find(
            product => product.ProductName.ToLower().Equals(payload.ProductName.ToLower()), new[] { "ProductPrices" });
        
        if (product is null)
        {
            var result = await _persistence.ExecuteTransactionAsync(async () =>
            {
                var product = await _productRepository.Save(payload);
                await _persistence.SaveChangesAsync();
           
                return product;
            });

            var productPriceResponses = result.ProductPrices.Select(productPrice => new ProductPriceResponse
            {
                Id = productPrice.Id.ToString(),
                Price = productPrice.Price,
                Stock = productPrice.Stock,
                StoreId = productPrice.StoreId.ToString()
            }).ToList();

            ProductResponse response = new()
            {
                Id = result.Id.ToString(),
                ProductName = result.ProductName,
                Description = result.Description,
                ProductPrices = productPriceResponses
            };

            return response;
        }

        var productPricesRequest = payload.ProductPrices.ToList();

        ProductPrice productPrice = new()
        {
            Price = productPricesRequest[0].Price,
            Stock = productPricesRequest[0].Stock,
            StoreId = productPricesRequest[0].StoreId,
        };

        product.ProductPrices.Add(productPrice);
        await _persistence.SaveChangesAsync();

        ProductResponse productResponse = new()
        {
            Id = product.Id.ToString(),
            ProductName = product.ProductName,
            Description = product.Description,
            ProductPrices = new List<ProductPriceResponse>
            {
                new()
                {
                    Id = productPrice.Id.ToString(),
                    Price = productPrice.Price,
                    Stock = productPrice.Stock,
                    StoreId = productPrice.StoreId.ToString()
                }
            }
        };

        return productResponse;
    }

    public async Task<ProductResponse> GetById(string id)
    {
        var product = await _productRepository.Find(product => product.Id.Equals(Guid.Parse(id)),
            new[] { "ProductPrices" });

        if (product is null) throw new Exception("product not found");

        var productPriceResponses = product.ProductPrices.Select(productPrice => new ProductPriceResponse
        {
            Id = productPrice.Id.ToString(),
            Price = productPrice.Price,
            Stock = productPrice.Stock,
            StoreId = productPrice.StoreId.ToString()
        }).ToList();

        ProductResponse response = new()
        {
            Id = product.Id.ToString(),
            ProductName = product.ProductName,
            Description = product.Description,
            ProductPrices = productPriceResponses
        };

        return response;
    }

    public async Task<PageResponse<ProductResponse>> GetAll(string? name, int page, int size)
    {
        var products = await _productRepository.FindAll(
            criteria: p => EF.Functions.Like(p.ProductName, $"%{name}%"),
            page: page,
            size: size,
            includes: new[] { "ProductPrices" }
        );
        
        var productResponses = products.Select(product =>
        {
            var productPriceResponses = product.ProductPrices.Select(productPrice =>
            {
                ProductPriceResponse productPriceResponse = new()
                {
                    Id = productPrice.Id.ToString(),
                    Price = productPrice.Price,
                    Stock = productPrice.Stock,
                    StoreId = productPrice.StoreId.ToString()
                };
                return productPriceResponse;
            }).ToList();

            return new ProductResponse
            {
                Id = product.Id.ToString(),
                ProductName = product.ProductName,
                Description = product.Description,
                ProductPrices = productPriceResponses
            };
        }).ToList();

        var totalPages = (int)Math.Ceiling(await _productRepository.Count() / (decimal)size);

        PageResponse<ProductResponse> pageResponse = new()
        {
            Content = productResponses,
            TotalPages = totalPages,
            TotalElement = productResponses.Count
        };

        return pageResponse;
        
    }

    public Task<ProductResponse> Update(Product payload)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteById(string id)
    {
        var product = await _productRepository.FindById(Guid.Parse(id));
        if (product is null) throw new Exception("product not found");
        _productRepository.Delete(product);
        await _persistence.SaveChangesAsync();
    }
}