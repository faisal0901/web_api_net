namespace TokonyadiaRestAPI.DTO;

public class ProductPriceResponse
{
    public string Id { get; set; }
    public long Price { get; set; }
    public int Stock { get; set; }
    public string StoreId { get; set; }
}