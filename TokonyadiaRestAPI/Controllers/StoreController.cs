using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokonyadiaEF.Entities;
using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Exception;
using TokonyadiaRestAPI.Exceptions;
using TokonyadiaRestAPI.Repositories;
using TokonyadiaRestAPI.Services;

namespace TokonyadiaRestAPI.Controllers;


[Route("api/stores")]
public class StoreController : BaseController
{
    private readonly IStoreService _storeService;
   

    public StoreController(IStoreService storeService)
    {
        _storeService = storeService;
        
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewStore([FromBody] Store store)
    {
        var entry = await _storeService.CreateNewStore(store);
        CommonResponse<StoreResponse> commondResponse = new()
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "successfully creat new Store",
            Data = entry
        };
        return Created("/api/stores", commondResponse);
    }

    [HttpGet]
 
    public async Task<IActionResult> GetAllStore()
    {
        var entry = await _storeService.GetAllStore();
        return Ok(entry);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStoreById(string id)
    {
        try
        {
            var entry = await _storeService.GetStoreById(id);
            CommonResponse<StoreResponse> commondResponse = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "successfully creat new Store",
                Data = entry
            };
            return Created("/api/stores", commondResponse);
        }
        catch (NotFoundException e)
        {
            return new StatusCodeResult(500);
        }
        catch (System.Exception e)
        {
            return new StatusCodeResult(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStore([FromBody] Store store)
    {
        try
        {
            var entry = await _storeService.UpdateStore(store);
            CommonResponse<StoreResponse> commondResponse = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "successfully creat new Store",
                Data = entry
            };
            return Ok(commondResponse);
        }
        catch (System.Exception e)
        {
    
            return new StatusCodeResult(500);
        }
    }

  

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStoreById(string id)
    {
        try
        {
             await _storeService.DeleteStoreById(id);
            CommonResponse<StoreResponse> commondResponse = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "successfully creat new Store",
                
            };
            return Created("/api/stores", commondResponse);
        }
        catch (System.Exception e)
        {
            
            return new StatusCodeResult(500);
        }
    }
}

