using System;
using System.Linq.Expressions;

namespace TestLib
{
    public class ClauseFactory
    {
        public static Clause CreateClause(MethodCallExpression expression)
        {
            if ("select".Equals(expression.Method.Name.ToLower()))
            {
                return new SelectClause(expression);
            }
            else if ("where".Equals(expression.Method.Name.ToLower()))
            {
                return new WhereClause(expression);
            }
            else if ("join".Equals(expression.Method.Name.ToLower()))
            {
                return new JoinClause(expression);
            }
            else if ("orderby".Equals(expression.Method.Name.ToLower()))
            {
                return new OrderByClause(expression, false);
            }
            else if ("orderbydescending".Equals(expression.Method.Name.ToLower()))
            {
                return new OrderByClause(expression, true);
            }

            return new OtherClause(expression);
        }
    }
}
