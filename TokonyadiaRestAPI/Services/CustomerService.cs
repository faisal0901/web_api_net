using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc;
using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Entities;
using TokonyadiaRestAPI.Repositories;

namespace TokonyadiaRestAPI.Services;

public class CustomerService:ICustomerService
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly ILogger _logger;
    private readonly IPersistence _persistence;

    public CustomerService(IRepository<Customer> customerRepository,IPersistence persistence)
    {
        _customerRepository = customerRepository;
        _persistence = persistence;
    }
    public async Task<CustomerResponse> CreateNewCustomer(Customer customer)
    {
        var entry = await _customerRepository.Save(customer);
        await _persistence.SaveChangesAsync();
        var customerResponse = new CustomerResponse()
        {
            id = entry.Id.ToString(),
            address = entry.Address,
            customer_name = entry.CustomerName,
            email = entry.Email,
            phone_number = entry.PhoneNumber
        };
        return customerResponse;
    }

    public async Task<List<CustomerResponse>> GetAllCustomer()
    {
        var entry = await _customerRepository.FindAll();
        await _persistence.SaveChangesAsync();
        var customerResponseList = new List<CustomerResponse>();
        foreach (var c in entry)
        {
            CustomerResponse customerResponse = new()
            {
                customer_name = c.CustomerName,
                id = c.Id.ToString(),
                address = c.Address,
                email = c.Email,
                phone_number = c.PhoneNumber
            };
            customerResponseList.Add(customerResponse);

        }
        return customerResponseList;
    }

    

    public Task<CustomerResponse> GetCustomerById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerResponse> UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerResponse> DeleteCustomerById(string id)
    {
        throw new NotImplementedException();
    }
}