using Core.Entities;
using Core.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec) {

            if (spec.Criteria != null) {
                query = query.Where(spec.Criteria);

            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (!string.IsNullOrWhiteSpace(spec.SortColumn) && IsValidProperty(spec.SortColumn))
            {
                var type = typeof(T);
                var parameter = Expression.Parameter(type, "x");
                var property = Expression.Property(parameter, spec.SortColumn);
                //var lambda = Expression.Lambda<Func<T, decimal>>(property, parameter);
                var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);
                query = query.OrderBy(lambda);
            }


            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

  
            return query;
        }

        public static bool IsValidProperty(string propertyName, bool throwExceptionIfNotFound = true)
        {
            var prop = typeof(T).GetProperty(
            propertyName,
            BindingFlags.IgnoreCase |
            BindingFlags.Public |
            BindingFlags.Static |
            BindingFlags.Instance);
            if (prop == null && throwExceptionIfNotFound)
            {
                throw new NotSupportedException($"ERROR: Property'{propertyName}' does not exist.");
            }
            return prop != null;
        }


    }
}
