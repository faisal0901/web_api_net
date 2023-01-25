using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Entities;
using TokonyadiaRestAPI.Exception;
using TokonyadiaRestAPI.Exceptions;
using TokonyadiaRestAPI.Repositories;
using TokonyadiaRestAPI.Services;

namespace TokonyadiaRestAPI.Controllers;


[Route("api/customers")]
public class CustomerController : BaseController
{
    private readonly ICustomerService _customerService;
  
    private readonly AppDbContext _appDbContext;

    public CustomerController(ICustomerService customerService,AppDbContext appDbContext)
    {
        _customerService = customerService;
        _appDbContext = appDbContext;

    }

    [HttpPost]
    public async Task<IActionResult> CreateNewCustomer([FromBody] Customer customer)
    {
        var entry = await _customerService.CreateNewCustomer(customer);

        CommonResponse<CustomerResponse> response = new()
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "successfully creat new customer",
            Data =entry
        };
        return Created("/api/customers", response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCustomer()
      {
        var customer =await _customerService.GetAllCustomer();
        // List<Customer> customer = await _appDbContext.Customers.ToListAsync();
        CommonResponse<List<Customer>> response = new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "successfully get customer",
            Data = (List<Customer>)customer
        };
        return Ok(response);
    }
    [HttpGet("me")]
    public async Task<IActionResult> GetMyself()
    {
        
        var email = User.Claims.FirstOrDefault(claim => claim.Type.Equals((ClaimTypes.Email)))?.Value;
        var customer = _appDbContext.Customers.Include("UserCredential")
            .FirstOrDefault(customer => customer.UserCredentials.Email.Equals((email)));
        if (customer is null) throw new UnathorizedException("unathorized");
        
        return Ok(customer);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(string id)
    {
        try
        {
            var customer = await  _customerService.GetCustomerById(id);
        
            if (customer is null) return NotFound("customer not found");
            
            CommonResponse<CustomerResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "successfully creat new customer",
                Data =customer
            };
            return Ok(response);
        }
        catch (NotFoundException e)
        {
          throw new NotFoundException("not found");
        }
        catch (System.Exception e)
        {
            return new StatusCodeResult(500);
        }
       
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCustomer([FromBody] Customer payload)
    {
        try
        {
            var customer = await _customerService.UpdateCustomer(payload);
            CommonResponse<CustomerResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "successfully creat new customer",
                Data =customer
            };
            return Ok(response);
        }
        catch (System.Exception e)
        {
            return new StatusCodeResult(500);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomerById(string id)
    {
       
        try
        {
             await _customerService.DeleteCustomerById(id);
             CommonResponse<CustomerResponse> response = new()
             {
                 StatusCode = (int)HttpStatusCode.Created,
                 Message = "successfully creat new customer"
             };
             return Ok(response);
        }
        catch (System.Exception e)
        {
            return new StatusCodeResult(500);
        }
    }
}