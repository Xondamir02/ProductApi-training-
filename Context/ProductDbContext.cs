using Microsoft.EntityFrameworkCore;
using ProductApi.Entites;

namespace ProductApi.Context;
public class ProductDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {

    }

}