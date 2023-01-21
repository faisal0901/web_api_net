using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokonyadiaRestAPI.Entities;
using TokonyadiaRestAPI.Repositories;

namespace TokonyadiaRestAPI.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger _logger;

    public CustomerController(AppDbContext appDbContext, ILogger<CustomerController> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewCustomer([FromBody] Customer customer)
    {
        var entry = await _appDbContext.Customers.AddAsync(customer);
        await _appDbContext.SaveChangesAsync();
        return Created("/api/customers", entry.Entity);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCustomer()
    {
        var customers = await _appDbContext.Customers.ToListAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(string id)
    {
        try
        {
            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(customer => customer.Id.Equals(Guid.Parse(id)));

            if (customer is null) return NotFound("customer not found");
            
            return Ok(customer);
        }
        catch (Exception e)
        {
            return new StatusCodeResult(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
    {
        try
        {
            if (customer.Id == Guid.Empty) return new NotFoundObjectResult("customer not found");
            var currentCustomer = await _appDbContext.Customers.FirstOrDefaultAsync(c => c.Id.Equals(customer.Id));
            if (currentCustomer is null) return new NotFoundObjectResult("customer not found");
            
            var entry = _appDbContext.Customers.Attach(customer);
            _appDbContext.Customers.Update(customer);
            
            await _appDbContext.SaveChangesAsync();
            return Ok(entry.Entity);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomerById(string id)
    {
        try
        {
            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(customer => customer.Id.Equals(Guid.Parse(id)));
            if (customer is null) return NotFound("customer not found");
            _appDbContext.Customers.Remove(customer);
            await _appDbContext.SaveChangesAsync();
            return Ok("customer successfully deleted");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new StatusCodeResult(500);
        }
    }
}