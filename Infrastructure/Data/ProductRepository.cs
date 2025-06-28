using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }
       
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
        {
            var query = _context.Products.AsQueryable();
            if(!string.IsNullOrWhiteSpace(brand)) 
                query = query.Where(b => b.Brand == brand);
            if (!string.IsNullOrWhiteSpace(type))
                query = query.Where(b => b.Type == type);

            query = sort switch
            {
                "priceAsc" => query.OrderBy(p => p.Price),
                "priceDesc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Name),
            };

            return await query.ToListAsync();
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public void UpdateProduct(Product product)
        {
           _context.Entry(product).State = EntityState.Modified;
        }

        public async Task<IReadOnlyList<string>> GetProductBrandsAsync()
        {
            return await _context.Products.Select(p => p.Brand).Distinct().ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetProductTypesAsync()
        {
            return await _context.Products.Select(p => p.Type).Distinct().ToListAsync();
        }
    }
}
