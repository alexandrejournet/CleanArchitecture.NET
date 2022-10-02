using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Specifications.Base
{
    public class BaseSpecifications<T> : IBaseSpecifications<T>
    {
        private readonly List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> _includeCollection = new();

        public BaseSpecifications() { }

        public BaseSpecifications(Expression<Func<T, bool>> filterCondition)
        {
            FilterCondition = filterCondition;
        }

        public Expression<Func<T, bool>> FilterCondition { get; private set; }
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes
        {
            get
            {
                return _includeCollection;
            }
        }

        public Expression<Func<T, object>> GroupBy { get; private set; }

        public void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected void SetFilterCondition(Expression<Func<T, bool>> filterExpression)
        {
            FilterCondition = filterExpression;
        }

        protected void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }
    }
}
