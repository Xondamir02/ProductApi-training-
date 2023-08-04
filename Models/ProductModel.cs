using ProductApi.Entites;

namespace ProductApi.Models;

public class ProductModel
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
}