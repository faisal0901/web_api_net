using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Entities;
using TokonyadiaRestAPI.Repositories;

namespace TokonyadiaRestAPI.Services;

public class PurchaseService:IPurchaseService
{
    private readonly IRepository<Purchase> _purchaseRepository;
    private readonly IRepository<PurchaseDetail> _purchaseDetailRepository;
    private readonly IRepository<Customer> _CustomerRepository;
    
    private readonly ILogger _logger;
    private readonly IPersistence _persistence;
    public  PurchaseService(IPersistence persistence,IRepository<Purchase> purchaseRepository,IRepository<PurchaseDetail> purchaseDetailRepository,IRepository<Customer> CustomerRepository)
    {
      
        _persistence = persistence;
        _purchaseRepository = purchaseRepository;
        _purchaseDetailRepository = purchaseDetailRepository;
        _CustomerRepository = CustomerRepository;
    }
    
     public async  Task<PurchaseResponse> CreateNewProduct(Purchase payload)
     {
         var customerCheck = await _CustomerRepository.Find(p => p.Id.Equals(Guid.Parse(payload.CustomerId.ToString())));
        if (customerCheck is null)
        {
            // return Redirect("api/customers")
          throw new Exception("customer not found");
        };
        var purchaseCheck = await _purchaseRepository.Find(p => p.CustomerId.Equals(Guid.Parse(payload.CustomerId.ToString())),includes:new string[]{"PurchaseDetails"});
        if (purchaseCheck is null)
        {
            var result = await _persistence.ExecuteTransactionAsync(async () =>
            {
                payload.TransDate=DateTime.Now;
                var purchase = await _purchaseRepository.Save(payload);
                await _persistence.SaveChangesAsync();
           
                return purchase;
            });
            var purchaseDetailResponse = payload.PurchaseDetails.Select(p => new PurchaseDetailResponse()
            {
                qty= p.Qty,
                ProductPriceId = p.ProductPriceId.ToString()
            }).ToList();
            PurchaseResponse response = new()
            {
                DateTime= result.TransDate,
                customerID = result.CustomerId.ToString(),
                purchaseDetail = purchaseDetailResponse,
                
            };
            return response;

        }
     
        var purchaseDetails = payload.PurchaseDetails.ToList();
       var  purchaseDetailResponsesTemp=new List<PurchaseDetailResponse>();
        foreach (var pd in purchaseDetails)
        {
            pd.PurchaseId = purchaseCheck.Id;
            PurchaseDetailResponse purchaseDetailResponse = new()
            {
                qty = pd.Qty,
                ProductPriceId = pd.PurchaseId.ToString(),

            };
            purchaseDetailResponsesTemp.Add(purchaseDetailResponse);

            _purchaseDetailRepository.Save(pd);
        
        }
        await _persistence.SaveChangesAsync();
        
        PurchaseResponse productResponse = new()
        {
            DateTime= purchaseCheck.TransDate,
            customerID = purchaseCheck.CustomerId.ToString(),
            purchaseDetail = purchaseDetailResponsesTemp,
            
        };
        return productResponse;
     }

     public Task<PageResponse<PurchaseResponse>> GetAll(string? name, int page, int size)
     {
         throw new NotImplementedException();
     }
}