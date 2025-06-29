

using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetListWithSpec(ISpecification<T> spec);
        Task<T?> GetByIdAsyncWithSpec(ISpecification<T> spec);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        bool Exists(int id);
        Task<bool> SaveAllChangesAsync();
        Task<int> CountAsync(ISpecification<T> spec);

    }
}
