using ProductApi.PaginationHelper;

namespace ProductApi.Models;

public class ProductFilter:PaginationParams
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? FromPrice { get; set; }
    public decimal? ToPrice { get; set; }
}