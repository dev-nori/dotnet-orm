using System;
using System.Linq;
using System.Linq.Expressions;

namespace TestLib
{
    public class QueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException("CreateQuery");
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new DbSet<TElement>(this, expression);
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException("Execute");
        }

        public TResult Execute<TResult>(Expression expression)
        {
            SqlTranslater sqlTranslater = new SqlTranslater();
            var result = sqlTranslater.Translate(expression);

            Sql sql = new Sql(expression);

            string query = sql.GetQuery();

            return default;
        }
    }
}
