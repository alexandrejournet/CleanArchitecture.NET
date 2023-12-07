using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Specifications
{
    public class Specification<T> : ISpecification<T>
    {
        public Specification() { }

        public Specification(Expression<Func<T, bool>> filterCondition)
        {
            FilterCondition = filterCondition;
        }

        public Expression<Func<T, bool>>? FilterCondition { get; private set; }
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }
        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; } = new();

        public Expression<Func<T, object>>? GroupBy { get; private set; }

        public void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        public void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        public void SetFilterCondition(Expression<Func<T, bool>> filterExpression)
        {
            FilterCondition = filterExpression;
        }

        public void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }
    }
}
