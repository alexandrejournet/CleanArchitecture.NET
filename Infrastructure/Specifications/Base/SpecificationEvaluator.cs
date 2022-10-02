namespace CleanArchitecture.Infrastructure.Specifications.Base
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, IBaseSpecifications<T>? specifications)
        {
            // Do not apply anything if specifications is null
            if (specifications == null)
            {
                return query;
            }

            // Modify the IQueryable
            // Apply filter conditions
            if (specifications.FilterCondition != null)
            {
                query = query.Where(specifications.FilterCondition);
            }

            // Includes
            query = specifications.Includes
                        .Aggregate(query, (current, include) => include(current));

            // Apply ordering
            if (specifications.OrderBy != null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            else if (specifications.OrderByDescending != null)
            {
                query = query.OrderByDescending(specifications.OrderByDescending);
            }

            // Apply GroupBy
            if (specifications.GroupBy != null)
            {
                query = query.GroupBy(specifications.GroupBy).SelectMany(x => x);
            }

            return query;
        }
    }
}
