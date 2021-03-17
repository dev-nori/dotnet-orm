using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace TestLib
{
    public static class SqlBuilderTemp
    {
        public static Expression Parse(Expression expression, int depth)
        {
            return expression.NodeType switch
            {
                ExpressionType.Constant => ParseConstant(expression, depth),
                ExpressionType.Call => ParseCall(expression, depth),
                ExpressionType.Quote => ParseUnary(expression, depth),
                ExpressionType.Lambda => ParseLambda(expression, depth),
                ExpressionType.NotEqual => ParseNotEqual(expression, depth),
                ExpressionType.Equal => ParseEqual(expression, depth),
                ExpressionType.OrElse => ParseOrElse(expression, depth),
                ExpressionType.AndAlso => ParseAndAlso(expression, depth),
                ExpressionType.GreaterThan => ParseGreaterThan(expression, depth),
                ExpressionType.MemberAccess => ParseMemberAccess(expression, depth),
                ExpressionType.Parameter => ParseParameter(expression, depth),
                ExpressionType.New => ParseNew(expression, depth),
                _ => WhatType(expression),
            };
        }

        private static Expression ParseGreaterThan(Expression expression, int depth)
        {
            BinaryExpression binary = (BinaryExpression)expression;

            Tab(depth);
            Console.WriteLine($"Greater than L : {binary.Left}");
            Parse(binary.Left, depth + 1);

            Tab(depth);
            Console.WriteLine($"Greater than R : {binary.Right}");
            Parse(binary.Right, depth + 1);

            return Expression.Empty();
        }

        private static Expression ParseAndAlso(Expression expression, int depth)
        {
            BinaryExpression binary = (BinaryExpression)expression;

            Tab(depth);
            Console.WriteLine($"And Also L : {binary.Left}");
            Parse(binary.Left, depth + 1);

            Tab(depth);
            Console.WriteLine($"And Also R : {binary.Right}");
            Parse(binary.Right, depth + 1);

            return Expression.Empty();
        }

        private static Expression ParseOrElse(Expression expression, int depth)
        {
            BinaryExpression binary = (BinaryExpression)expression;

            Tab(depth);
            Console.WriteLine($"Else L : {binary.Left}");
            Parse(binary.Left, depth + 1);

            Tab(depth);
            Console.WriteLine($"Else R : {binary.Right}");
            Parse(binary.Right, depth + 1);

            return Expression.Empty();
        }

        private static Expression ParseNew(Expression expression, int depth)
        {
            NewExpression newExp = (NewExpression)expression;

            Tab(depth);
            Console.WriteLine($"New");
            for (int i = 0; i < newExp.Members.Count; i++)
            {
                Tab(depth);
                
                Console.WriteLine($" {newExp.Members[i].Name}:{newExp.Arguments[i]}");
                Parse(newExp.Arguments[i], depth + 1);
            }

            return Expression.Empty();
        }

        private static Expression ParseParameter(Expression expression, int depth)
        {
            ParameterExpression parameter = (ParameterExpression)expression;
            Tab(depth);
            Console.WriteLine($"Param : {parameter.Name}");

            return parameter;
        }

        private static Expression ParseMemberAccess(Expression expression, int depth)
        {
            MemberExpression member = (MemberExpression)expression;
            Tab(depth);
            Console.WriteLine($"Member : {member.Member.Name} {member.Member.DeclaringType}");

            return member.Expression != null ? Parse(member.Expression, depth + 1) : Expression.Empty();
        }

        private static Expression ParseEqual(Expression expression, int depth)
        {
            var notEqual = (BinaryExpression)expression;
            Tab(depth);
            Console.WriteLine($"Equal : {notEqual.Left} == {notEqual.Right}");

            Parse(notEqual.Left, depth + 1);
            Parse(notEqual.Right, depth + 1);

            return Expression.Empty();
        }

        private static Expression ParseNotEqual(Expression expression, int depth)
        {
            var notEqual = (BinaryExpression)expression;
            Tab(depth);
            Console.WriteLine($"NotEqual : {notEqual.Left} != {notEqual.Right}");

            Parse(notEqual.Left, depth + 1);
            Parse(notEqual.Right, depth + 1);

            return Expression.Empty();
        }

        private static Expression ParseLambda(Expression expression, int depth)
        {
            LambdaExpression lambda = (LambdaExpression)expression;
            Tab(depth);
            Console.WriteLine($"Lambda : {lambda.Body}");
            foreach (var p in lambda.Parameters)
            {
                Tab(depth);
                Console.WriteLine($"   : {p}");
                Parse(p, depth + 1);
            }

            return Parse(lambda.Body, depth + 1);
        }

        private static Expression ParseUnary(Expression expression, int depth)
        {
            UnaryExpression unary = (UnaryExpression)expression;
            Tab(depth);
            Console.WriteLine($"Unary : {unary.Operand}");

            return Parse(unary.Operand, depth + 1);
        }

        private static Expression ParseCall(Expression expression, int depth)
        {
            MethodCallExpression methodCall = (MethodCallExpression)expression;

            Tab(depth);
            Console.WriteLine("method: " + methodCall.Method.Name);

            foreach (var exp in methodCall.Arguments)
            {
                Parse(exp, depth + 1);
            }

            return Expression.Empty();
        }

        private static Expression ParseConstant(Expression expression, int depth)
        {
            ConstantExpression constant = (ConstantExpression)expression;

            Tab(depth);
            Console.WriteLine("constant : " + constant.Value);

            return Expression.Empty();
        }

        private static Expression WhatType(Expression expression)
        {
            if (expression != null)
            {
                Console.WriteLine("type is " + expression.NodeType);
            }

            return Expression.Empty();
        }

        public static Expression Translate(Expression expression)
        {
            return Parse(expression, 0);
        }

        static void Tab(int tab)
        {
            for (int i = 0; i < tab; i++)
            {
                Console.Write(" . ");
            }
        }
    }
}
