using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Specifications.Base
{
    public interface IBaseSpecifications<T>
    {
        // Filter Conditions
        Expression<Func<T, bool>> FilterCondition { get; }

        // Order By Ascending
        Expression<Func<T, object>> OrderBy { get; }

        // Order By Descending
        Expression<Func<T, object>> OrderByDescending { get; }

        // Include collection to load related data
        List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; }

        // GroupBy expression
        Expression<Func<T, object>> GroupBy { get; }
    }
}