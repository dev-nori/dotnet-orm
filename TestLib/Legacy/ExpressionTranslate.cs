using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TestLib
{
    public class ExpressionTranslate
    {
        public static Type ParseNewType(SelectClause selectClause)
        {
            UnaryExpression unary = (UnaryExpression) selectClause.InnerExpression
                .FirstOrDefault(c => c.NodeType == ExpressionType.Quote);

            LambdaExpression lambda = (LambdaExpression)unary.Operand;

            return lambda.ReturnType;
        }

        public static Type ParseJoinType(JoinClause joinClause)
        {
            ConstantExpression constant = (ConstantExpression) joinClause.InnerExpression
                .FirstOrDefault(c => c.NodeType == ExpressionType.Constant);

            Type genericDbset = constant.Value.GetType();
            return genericDbset.GetGenericArguments().First();
        }

        public static Type ParseWhereType(WhereClause whereClause)
        {
            ConstantExpression constant = (ConstantExpression)whereClause.InnerExpression
                .FirstOrDefault(c => c.NodeType == ExpressionType.Constant);

            Type genericDbset = constant.Value.GetType();
            return genericDbset.GetGenericArguments().First();
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

        public static void Translate(SelectClause clause)
        {
            
        }

        public static void Translate(WhereClause clause)
        {

        }

        public static void Translate(JoinClause clause)
        {

        }
    }
}
