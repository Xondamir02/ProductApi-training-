using ProductApi.Models;

namespace ProductApi.Entites;

public class Product
{
    public  Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
}

public static class ProductExtension
{
    public static ProductModel ToModel(this Product product)
    {
        return new ProductModel()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
        };
    }
}