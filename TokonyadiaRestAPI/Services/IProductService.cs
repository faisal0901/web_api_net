using TokonyadiaEF.Entities;
using TokonyadiaRestAPI.DTO;

namespace TokonyadiaRestAPI.Services;

public interface IProductService
{
    Task<ProductResponse> CreateNewProduct(Product payload);
    Task<ProductResponse> GetById(string id);
    Task<PageResponse<ProductResponse>> GetAll(string? name, int page, int size);
    Task<ProductResponse> Update(Product payload);
    Task DeleteById(string id);
}