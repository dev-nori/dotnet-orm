using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace TestLib
{
    public class Clause
    {
        public MethodCallExpression ClauseExpression { get; }

        public List<Expression> InnerExpression { get; }

        public Clause(Expression expression)
        {
            if (expression is MethodCallExpression methodCall)
            {
                ClauseExpression = methodCall;
                InnerExpression = methodCall.Arguments
                    .Where(e => e.NodeType != ExpressionType.Call).ToList();
            }
            else
            {
                throw new InvalidExpressionException("Only Method Call Expression");
            }
        }

        public MethodCallExpression GetNextClause()
        {
            return (MethodCallExpression)ClauseExpression.Arguments
                .FirstOrDefault(e => e.NodeType == ExpressionType.Call);
        }

        public override string ToString()
        {
            return $"{ClauseExpression.Method.Name}";
        }
    }

    public class SelectClause : Clause
    {
        public SelectClause(Expression expression) : base(expression) { }
    }

    public class WhereClause : Clause
    {
        public WhereClause(Expression expression) : base(expression) { }
    }

    public class JoinClause : Clause
    {
        public JoinClause(Expression expression) : base(expression) { }
    }

    public class OrderByClause : Clause
    {
        public bool Descending { get; }
        public OrderByClause(Expression expression, bool descending) : base(expression)
        {
            Descending = descending;
        }
    }
}