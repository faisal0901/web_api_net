using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Entities;
using TokonyadiaRestAPI.Exception;
using TokonyadiaRestAPI.Exceptions;
using TokonyadiaRestAPI.Repositories;

namespace TokonyadiaRestAPI.Services;

public class CustomerService:ICustomerService
{
    private readonly IRepository<Customer> _customerRepository;

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
     
            phone_number = entry.PhoneNumber
        };
        return customerResponse;
    }

  


    public async Task<IEnumerable<Customer>> GetAllCustomer()
    {
        var entry = await _customerRepository.FindAll();
        return entry;
    }

    

    public async Task<CustomerResponse> GetCustomerById(string id)
    {
        var customer = await _customerRepository.Find(customer => customer.Id.Equals(Guid.Parse(id)));
        if (customer is null)
        {
            return null;
        }
        CustomerResponse customerResponse = new()
        {
            customer_name =  customer.CustomerName,
            id =  customer.Id.ToString(),
            address =  customer.Address,
   
            phone_number =  customer.PhoneNumber
        };
        return customerResponse;

    }

    public  async  Task<CustomerResponse> UpdateCustomer(Customer customer)
    {
        if (customer.Id == Guid.Empty) throw new NotFoundException("not found");
        var currentCustomer = await _customerRepository.Find(c => c.Id.Equals(customer.Id));
        if (currentCustomer is null) return null;
            
        var entry = _customerRepository.Attach(customer);
        _customerRepository.Update(customer);
            
        await _persistence.SaveChangesAsync();
        CustomerResponse customerResponse = new()
        {
            customer_name = entry.CustomerName,
            id = entry.Id.ToString(),
            address = entry.Address,
       
            phone_number = entry.PhoneNumber
        };
        return customerResponse;
    }

  

    public async Task DeleteCustomerById(string id)
    {
        var customer = await _customerRepository.Find(customer => customer.Id.Equals(Guid.Parse(id)));
        if (customer is null) throw new NotFoundException("not found");
        _customerRepository.Delete(customer);
        await _persistence.SaveChangesAsync();
    }
}