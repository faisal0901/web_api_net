using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokonyadiaEF.Entities;
using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Entities;
using TokonyadiaRestAPI.Repositories;
using TokonyadiaRestAPI.Services;


namespace TokonyadiaRestAPI.Controllers;
[ApiController]
[Route("api/purchases")]
public class PurchaseController:ControllerBase
{
    private readonly IPurchaseService _purchaseService;
    
    public  PurchaseController(IPurchaseService purchaseService)
    {

        _purchaseService = purchaseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewPurchase([FromBody] Purchase payload)
    {
        var purchaseResponse = await _purchaseService.CreateNewPurchase(payload);

        CommonResponse<PurchaseResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "successfully creat new customer",
            Data = purchaseResponse
        };

        return Created("/api/purchases", response);
       
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var purchaseResponse = await _purchaseService.GetById(id);

        CommonResponse<PurchaseResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "succes create by id",
            Data = purchaseResponse
        };

        return Ok(response);
    }
 
}