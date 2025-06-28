using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        //Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? types, string? sort);
        Task<Product?> GetProductByIdAsync(int id);
        void AddProduct(Product product);
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
        bool ProductExists(int id);
        Task<bool> SaveChangesAsync();
        Task<IReadOnlyList<string>> GetProductBrandsAsync();
        Task<IReadOnlyList<string>> GetProductTypesAsync();
    }
}
