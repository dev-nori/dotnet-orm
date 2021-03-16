using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace TestLib
{
    public class ExpressionTranslate
    {
        public static Expression Translate(Expression expression)
        {
            return expression;
        }

        public static Expression ParseMethod(Expression expression)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Clause> ParseClause(Expression expression)
        {
            if (expression is MethodCallExpression methodCall)
            {
                var result = new List<Clause>();
                Clause clause = ClauseFactory.CreateClause(methodCall);
                result.Add(clause);
                while ((methodCall = clause.GetNextClause()) != null)
                {
                    Clause nextClause = ClauseFactory.CreateClause(methodCall);

                    result.Add(nextClause);

                    clause = nextClause;
                }

                return result;
            }
            else
            {
                throw new InvalidExpressionException("Only Method Call Expression");
            }
        }
    }
}
