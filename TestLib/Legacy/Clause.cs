using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace TestLib
{
    public enum ClauseType
    {
        Other, Select, OrderBy, Join, Where
    }

    public abstract class Clause
    {
        public abstract ClauseType ClauseType { get; }

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

    public class OtherClause : Clause
    {
        public override ClauseType ClauseType => ClauseType.Other;

        public OtherClause(Expression expression) : base(expression) { }
    }

    public class SelectClause : Clause
    {
        public override ClauseType ClauseType => ClauseType.Select;

        public SelectClause(Expression expression) : base(expression) { }
    }

    public class WhereClause : Clause
    {
        public override ClauseType ClauseType => ClauseType.Where;

        public WhereClause(Expression expression) : base(expression) { }
    }

    public class JoinClause : Clause
    {
        public override ClauseType ClauseType => ClauseType.Join;

        public JoinClause(Expression expression) : base(expression) { }
    }

    public class OrderByClause : Clause
    {
        public override ClauseType ClauseType => ClauseType.OrderBy;

        public bool Descending { get; }

        public OrderByClause(Expression expression, bool descending) : base(expression)
        {
            Descending = descending;
        }
    }
}