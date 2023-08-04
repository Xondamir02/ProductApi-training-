using Microsoft.EntityFrameworkCore;
using ProductApi.Context;
using ProductApi.Entites;
using ProductApi.Exceptions;
using ProductApi.Exeptions;
using ProductApi.Models;
using ProductApi.PaginationHelper;
using ProductApi.Validator;

namespace ProductApi.Managers;

public class ProductManager:IProductManager
{
    private readonly ProductDbContext _context;

    public ProductManager(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductModel>> GetProductsAsync(ProductFilter filter)
    {
        var query=_context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(p => p.Name.Contains(filter.Name));
        if (!string.IsNullOrWhiteSpace(filter.Description))
            query = query.Where(p => p.Description.Contains(filter.Description));
        if (filter.FromPrice is not null)
            query = query.Where(p => p.Price > filter.FromPrice);
        if (filter.ToPrice is not null)
            query = query.Where(p => p.Price < filter.ToPrice);
        return await query.Select(p => p.ToModel()).ToPagedListAsync(filter);
    }

    public async Task<ProductModel> GetProductByIdAsync(Guid id)
    {
        var project = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
        if (project == null)
        {
            throw new ProductNotFoundException(id.ToString());
        }

        return project.ToModel();
    }

    public async Task<ProductModel> GetProductByNameAsync(string name)
    {
        var project = await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        if (project == null)
             throw new ProductNotFoundException(name);

        return project.ToModel();
    }

    public async Task<ProductModel> AddProductAsync(CreateProductModel model)
    {
        var product = new Product()
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price
        };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product.ToModel();
    }

    public async Task<ProductModel> UpdateProductAsync(Guid productId, CreateProductModel model)
    {
        var validator = new CreateProductValidator();
        var result = validator.Validate(model);
        if (!result.IsValid)
        {
            throw new IsNotValidException("Product Model");
        }
        var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == productId);
        if (product == null)
            throw new ProductNotFoundException(productId.ToString());
        product.Name=model.Name;
        product.Description=model.Description;
        product.Price=model.Price;
        await _context.SaveChangesAsync();
        return product.ToModel();
    }

    public async Task DeleteProductAsync(Guid productId)
    {
        var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == productId);
        if (product == null)
            throw new ProductNotFoundException(productId.ToString());
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}