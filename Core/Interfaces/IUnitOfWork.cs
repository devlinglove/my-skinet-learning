using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Complete();
        IGenericRepository<T> Repository<T>() where T : BaseEntity;

    }
}
