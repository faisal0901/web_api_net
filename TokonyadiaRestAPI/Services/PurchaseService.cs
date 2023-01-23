using Microsoft.AspNetCore.Mvc;
using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Entities;
using TokonyadiaRestAPI.Repositories;

namespace TokonyadiaRestAPI.Services;

public class PurchaseService:IPurchaseService
{
    private readonly IRepository<Purchase> _purchaseRepository;
    
    private readonly IRepository<Customer> _CustomerRepository;
    
    private readonly ILogger _logger;
    private readonly IPersistence _persistence;
    public  PurchaseService(IPersistence persistence,IRepository<Purchase> purchaseRepository,IRepository<PurchaseDetail> purchaseDetailRepository,IRepository<Customer> CustomerRepository)
    {
      
        _persistence = persistence;
        _purchaseRepository = purchaseRepository;
        _CustomerRepository = CustomerRepository;
    }

    public async Task<PurchaseResponse> CreateNewPurchase(Purchase payload)
    {
        var customerCheck = await _CustomerRepository.Find(p => p.Id.Equals(Guid.Parse(payload.CustomerId.ToString())));
        if (customerCheck is null)
        {
            // return Redirect("api/customers")
            throw new Exception("customer not found");
        }

        ;
        var purchaseCheck = await _purchaseRepository.Find(
            p => p.CustomerId.Equals(Guid.Parse(payload.CustomerId.ToString())),
            includes: new string[] { "PurchaseDetails" });
        if (purchaseCheck is null)
        {
            var result = await _persistence.ExecuteTransactionAsync(async () =>
            {
                payload.TransDate = DateTime.Now;
                var purchase = await _purchaseRepository.Save(payload);
                await _persistence.SaveChangesAsync();

                return purchase;
            });
            var purchaseDetailResponse = payload.PurchaseDetails.Select(p => new PurchaseDetailResponse()
            {
                qty = p.Qty,
                ProductPriceId = p.ProductPriceId.ToString()
            }).ToList();
            PurchaseResponse response = new()
            {
                DateTime = result.TransDate,
                customerID = result.CustomerId.ToString(),
                purchaseDetail = purchaseDetailResponse,

            };
            return response;

        }

        var purchaseDetails = payload.PurchaseDetails.ToList();
        var purchaseDetailResponsesTemp = new List<PurchaseDetailResponse>();
        foreach (var pd in purchaseDetails)
        {
            pd.PurchaseId = purchaseCheck.Id;
            PurchaseDetailResponse purchaseDetailResponse = new()
            {
                qty = pd.Qty,
                ProductPriceId = pd.PurchaseId.ToString(),

            };
            purchaseDetailResponsesTemp.Add(purchaseDetailResponse);

            purchaseCheck.PurchaseDetails.Add(pd);

        }

        await _persistence.SaveChangesAsync();

        PurchaseResponse purchaseResponse = new()
        {
            DateTime = purchaseCheck.TransDate,
            customerID = purchaseCheck.CustomerId.ToString(),
            purchaseDetail = purchaseDetailResponsesTemp,

        };
        return purchaseResponse;
    }

    public async Task<PurchaseResponse> GetById(string id)
    {
        var purchaseCheck = await _purchaseRepository.Find(p => p.CustomerId.Equals(Guid.Parse(id)),
            includes: new string[] { "PurchaseDetails" });
        if (purchaseCheck is null) throw new Exception("customer not found");
        var purchaseDetails = purchaseCheck.PurchaseDetails.Select(p => new PurchaseDetailResponse()
        {
        qty = p.Qty,
        ProductPriceId = p.ProductPriceId.ToString()
        }).ToList();
   
        PurchaseResponse purchaseResponse = new()
        {
            DateTime = purchaseCheck.TransDate,
            customerID = purchaseCheck.CustomerId.ToString(),
            purchaseDetail = purchaseDetails
        };
        return purchaseResponse;
    }




 
}