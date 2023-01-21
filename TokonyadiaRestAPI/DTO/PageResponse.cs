namespace TokonyadiaRestAPI.DTO;

public class PageResponse<T>
{
    public List<T> Content { get; set; }
    public int TotalPages { get; set; }
    public int TotalElement { get; set; }
}