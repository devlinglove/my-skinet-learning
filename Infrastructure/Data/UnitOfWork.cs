using Core.Entities;
using Core.Interfaces;
using System.Collections.Concurrent;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        public ConcurrentDictionary<string, object> _repositories { get; set; } = new();

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;

            return (IGenericRepository<T>)_repositories.GetOrAdd(type, t =>
            {
                var respType = typeof(IGenericRepository<>).MakeGenericType(typeof(T));
                return Activator.CreateInstance(respType) ?? new InvalidOperationException($"Could not create repository of type {t}");
            });

        }
    }
}
