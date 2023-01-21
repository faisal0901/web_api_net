namespace TokonyadiaRestAPI.DTO;

public class ProductResponse
{
    public string Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }

    public List<ProductPriceResponse> ProductPrices { get; set; }
}