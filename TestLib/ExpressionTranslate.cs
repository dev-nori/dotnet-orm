using System;
using System.Linq;
using System.Linq.Expressions;

namespace TestLib
{
    public class ExpressionTranslate
    {
        public static string Parse(Expression expression, int depth)
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

        private static string ParseGreaterThan(Expression expression, int depth)
        {
            BinaryExpression binary = (BinaryExpression)expression;

            return MakeResult(depth, $"GreaterThan L : {binary.Left}")
                + Parse(binary.Left, depth + 1)
                + MakeResult(depth, $"GreaterThan R : {binary.Right}")
                + Parse(binary.Right, depth + 1);
        }

        private static string ParseAndAlso(Expression expression, int depth)
        {
            BinaryExpression binary = (BinaryExpression)expression;

            return MakeResult(depth, $"AndAlso L : {binary.Left}")
                + Parse(binary.Left, depth + 1)
                + MakeResult(depth, $"AndAlso R : {binary.Right}")
                + Parse(binary.Right, depth + 1);
        }

        private static string ParseOrElse(Expression expression, int depth)
        {
            BinaryExpression binary = (BinaryExpression)expression;

            return MakeResult(depth, $"Else L : {binary.Left}")
                + Parse(binary.Left, depth + 1)
                + MakeResult(depth, $"Else R : {binary.Right}")
                + Parse(binary.Right, depth + 1);
        }

        private static string ParseNew(Expression expression, int depth)
        {
            NewExpression newExp = (NewExpression)expression;

            var result = MakeResult(depth, "New");
            
            for (int i = 0; i < newExp.Members.Count; i++)
            {
                result += Parse(newExp.Arguments[i], depth + 1);
            }

            return result;
        }

        private static string ParseParameter(Expression expression, int depth)
        {
            ParameterExpression parameter = (ParameterExpression)expression;
            var result = MakeResult(depth, $"Param : {parameter.Name}");
            return result;
        }

        private static string ParseMemberAccess(Expression expression, int depth)
        {
            MemberExpression member = (MemberExpression)expression;
            var result = MakeResult(depth, $"Member : {member.Member.Name}, {member.Member.DeclaringType}");

            return member.Expression != null ?
                result + Parse(member.Expression, depth + 1) :
                result;
        }

        private static string ParseEqual(Expression expression, int depth)
        {
            var notEqual = (BinaryExpression)expression;
            var result = MakeResult(depth, $"Equal : {notEqual.Left} != {notEqual.Right}");

            return result += (Parse(notEqual.Left, depth + 1) + Parse(notEqual.Right, depth + 1));
        }

        private static string ParseNotEqual(Expression expression, int depth)
        {
            var notEqual = (BinaryExpression)expression;
            var result = MakeResult(depth, $"NotEqual : {notEqual.Left} != {notEqual.Right}");

            return result += (Parse(notEqual.Left, depth + 1) + Parse(notEqual.Right, depth + 1));
        }

        private static string ParseLambda(Expression expression, int depth)
        {
            LambdaExpression lambda = (LambdaExpression)expression;

            var result = MakeResult(depth, $"Lambda : {lambda.Body}");
            foreach (var p in lambda.Parameters)
            {
                result += Parse(p, depth + 1);
            }

            return result += Parse(lambda.Body, depth + 1);
        }

        private static string ParseUnary(Expression expression, int depth)
        {
            UnaryExpression unary = (UnaryExpression)expression;

            var result = MakeResult(depth, $"Unary : {unary.Operand}");
            return result + Parse(unary.Operand, depth + 1);
        }

        private static string ParseCall(Expression expression, int depth)
        {
            MethodCallExpression methodCall = (MethodCallExpression)expression;

            string result = MakeResult(depth, "method: " + methodCall.Method.Name);
            foreach (var exp in methodCall.Arguments)
            {
                result += Parse(exp, depth + 1);
            }

            return result;
        }

        private static string ParseConstant(Expression expression, int depth)
        {
            ConstantExpression constant = (ConstantExpression)expression;

            return MakeResult(depth, "constant : " + constant.Value);
        }

        private static string MakeResult(int depth, string v)
        {
            var result = Tab(depth) + v;
            return $"{result}\n";
        }

        private static string WhatType(Expression expression)
        {
            if (expression != null)
            {
                return "type is " + expression.NodeType;
            }

            return string.Empty;
        }

        public static string Translate(Expression expression)
        {
            return Parse(expression, 0);
        }

        static string Tab(int tab)
        {
            string result = "";
            for (int i = 0; i < tab; i++)
            {
                result += "  ";
            }
            result += $"[{tab}] : ";

            return result;
        }

    }
}
