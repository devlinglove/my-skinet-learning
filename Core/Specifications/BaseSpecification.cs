using Core.Interfaces;
using System.Linq.Expressions;
namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        private readonly Expression<Func<T, bool>>? _criteria;
        public BaseSpecification(Expression<Func<T, bool>>? criteria)
        {
            _criteria = criteria;
        }

        protected BaseSpecification() : this(null) { }

        public Expression<Func<T, bool>>? Criteria => _criteria;

        public Expression<Func<T, object>>? OrderBy {get; private set;}

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }


        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        public void AddOrderByDescending(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDescending = orderByDesc;
        }

    }
}
