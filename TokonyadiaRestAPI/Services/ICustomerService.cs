using Microsoft.AspNetCore.Mvc;
using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Entities;

namespace TokonyadiaRestAPI.Services;

public interface ICustomerService
{
    Task<CustomerResponse> CreateNewCustomer(Customer customer);
    Task<List<CustomerResponse>> GetAllCustomer();
    Task<CustomerResponse> GetCustomerById(string id);
    Task<CustomerResponse> UpdateCustomer(Customer customer);
    Task<CustomerResponse> DeleteCustomerById(string id);
}