using Core.Interfaces;
using System.Linq.Expressions;
using System.Reflection;
namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        private readonly Expression<Func<T, bool>>? _criteria;
        public BaseSpecification(Expression<Func<T, bool>>? criteria)
        {
            _criteria = criteria;
            SortColumn = "Name";
            SortOrder = "ASC";
        }

        protected BaseSpecification() : this(null) { }

        public Expression<Func<T, bool>>? Criteria { get; private set; }

        public Expression<Func<T, object>>? OrderBy {get; private set;}

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public int Skip  {get; private set;}

        public int Take {get; private set;}

        public bool IsPagingEnabled { get; private set; }

        public string SortColumn { get; private set; }

        public string SortOrder { get; private set; }

        public Dictionary<string, string> QueryParams { get; private set; }

        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        public IQueryable<T> ApplyCriteria(IQueryable<T> query)
        {
            if(Criteria != null)
            {
                query = query.Where(Criteria);
            }

            return query;
        }

        protected void ApplyDynamicFiltering(Dictionary<string, string> queryParams)
        {
            QueryParams = queryParams;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDescending = orderByDesc;
        }

        protected void ApplyOrdering(string? sortColumn, string? sortOrder)
        {
            SortColumn = sortColumn ?? "Name";
            SortOrder = sortOrder ?? "ASC";
        }
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        public void SetDynamicCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
    }
}
