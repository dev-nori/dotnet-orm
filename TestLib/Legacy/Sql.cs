using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace TestLib
{
    public class Sql
    {
        private List<Clause> Clauses { get; } = new List<Clause>();

        public Expression Expression { get; }

        public Sql(Expression expression)
        {
            Expression = expression;
            ParseExpression();
        }

        private void ParseExpression()
        {
            var expression = Expression;

            Console.WriteLine(ExpressionTranslateTemp.Parse(expression, 0));
            Clauses.AddRange(ExpressionTranslate.ParseClause(expression));
        }

        public string GetQuery()
        {
            var queryGenerator = new SqlGenerator(Clauses);
            return queryGenerator.Generate();
        }
    }
}
