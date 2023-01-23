using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokonyadiaEF.Entities;
using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Repositories;
using TokonyadiaRestAPI.Services;

namespace TokonyadiaRestAPI.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewPurchase([FromBody] Product request)
    {
        var productResponse = await _productService.CreateNewProduct(request);

        CommonResponse<ProductResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "successfully creat new customer",
            Data = productResponse
        };

        return Created("/api/products", response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(string id)
    {
        var productResponse = await _productService.GetById(id);
        
        CommonResponse<ProductResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "successfully get product",
            Data = productResponse
        };
        
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProduct([FromQuery] string? name, [FromQuery] int page = 1, [FromQuery] int size = 5)
    {
        var products = await _productService.GetAll(name, page, size);
        
        CommonResponse<PageResponse<ProductResponse>> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "successfully get product",
            Data = products
        };
        
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductById(string id)
    {
        await _productService.DeleteById(id);

        CommonResponse<string?> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "successfully delete customer",
        };

        return Ok(response);
    }
}