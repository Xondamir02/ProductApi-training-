using Microsoft.EntityFrameworkCore;
using ProductApi.Context;
using ProductApi.Entites;
using ProductApi.Models;

namespace ProductApi.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void MigrateProductDb(this WebApplication app)
        {
            var productDb = app.Services.GetService<ProductDbContext>();

            if (productDb is not null)
            {
                productDb.Database.Migrate();
            }
        }
    }
}
