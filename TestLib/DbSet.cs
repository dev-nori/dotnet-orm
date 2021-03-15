using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TestLib
{
    public class DbSet<T> : IQueryable<T>, IOrderedQueryable<T>
    {
        public DbSet(IQueryContext queryContext)
        {
            Provider = queryContext.Provider;
            Expression = Expression.Constant(this);
        }

        internal DbSet(IQueryProvider queryProvider, Expression expression)
        {
            Provider = queryProvider;
            Expression = expression;
        }

        public Type ElementType => typeof(T);

        public Expression Expression { get; }

        public IQueryProvider Provider { get; }

        public IEnumerator<T> GetEnumerator()
        {
            Provider.Execute<IEnumerable<T>>(Expression);
            return (IEnumerator<T>)Enumerable.Empty<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
