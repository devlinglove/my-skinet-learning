

using Core.Entities;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.Products.Any()) {

                var products = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

                var jsonProducts = JsonSerializer.Deserialize<List<Product>>(products);

                if (jsonProducts == null) return;

                context.AddRange(jsonProducts);

                await context.SaveChangesAsync();   

            }
        }
    }
}
