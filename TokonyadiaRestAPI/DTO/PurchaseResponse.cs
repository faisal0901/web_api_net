namespace TokonyadiaRestAPI.DTO;

public class PurchaseResponse
{
    public DateTime DateTime { get; set; }
    public string customerID  { get; set; }
    public List<PurchaseDetailResponse> purchaseDetail { get; set; }
}
