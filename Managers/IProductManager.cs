using ProductApi.Models;

namespace ProductApi.Managers;

public interface IProductManager
{
    Task<IEnumerable<ProductModel>>GetProductsAsync(ProductFilter  filter);
    Task<ProductModel> GetProductByIdAsync(Guid id);
    Task<ProductModel> GetProductByNameAsync(string name);
    Task<ProductModel>AddProductAsync(CreateProductModel product);
    Task<ProductModel> UpdateProductAsync(Guid productId,CreateProductModel product);
    Task DeleteProductAsync(Guid id);
}