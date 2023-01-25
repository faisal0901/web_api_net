using Microsoft.AspNetCore.Mvc;
using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Entities;

namespace TokonyadiaRestAPI.Services;

public interface ICustomerService
{
    Task<CustomerResponse> CreateNewCustomer(Customer customer);
    Task<IEnumerable<Customer>> GetAllCustomer();
    Task<CustomerResponse> GetCustomerById(string id);
    Task<CustomerResponse> UpdateCustomer(Customer customer);
    Task DeleteCustomerById(string id);
}