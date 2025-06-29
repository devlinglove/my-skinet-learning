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

            if (spec.QueryParams.Count > 0)
            {
                var param = Expression.Parameter(typeof(T), "p");
                if (spec.QueryParams.Count > 0)
                {
                    spec.QueryParams.Remove("pageIndex");
                    spec.QueryParams.Remove("pageSize");
                }
        
                Expression? body = null;
                foreach (var pair in spec.QueryParams)
                {
                    var member = Expression.Property(param, pair.Key);
                    var constant = Expression.Constant(pair.Value);
                    var expression = Expression.Equal(member, constant);
                    body = body == null ? expression : Expression.AndAlso(body, expression);
                }

                var lambda = Expression.Lambda<Func<T, bool>>(body, param);
                spec.SetDynamicCriteria(lambda);

                query = query.Where(lambda);

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
