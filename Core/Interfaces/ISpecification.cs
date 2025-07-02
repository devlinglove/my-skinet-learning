

using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get;  }
        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDescending { get; }
        int Skip { get; }
        int Take { get; }
        bool IsPagingEnabled { get; }
        string? SortColumn { get; }
        string? SortOrder { get; }
        public List<Expression<Func<T, object>>> Includes { get; }

        public List<string> IncludeStrings { get; }

        IQueryable<T> ApplyCriteria(IQueryable<T> query);

        public Dictionary<string, string> QueryParams { get; }

        public void SetDynamicCriteria(Expression<Func<T, bool>> criteria);


    }
}
