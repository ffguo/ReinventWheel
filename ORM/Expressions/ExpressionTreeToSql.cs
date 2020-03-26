using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace ORM.Expressions
{
    public class ExpressionTreeToSql
    {
        private static string sql = string.Empty;

        public static string GetSql<T>(Expression expression)
        {
            GenerateSelectHeader<T>();
            VisitExpression(expression);
            sql = sql.Trim();
            if (sql.EndsWith("and"))
                sql = sql.Substring(0, sql.Length - 3);
            return sql;
        }

        public static void VisitExpression(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Call:
                    MethodCallExpression exp = expression as MethodCallExpression;
                    if (exp != null)
                    {
                        if (!sql.Contains(exp.Method.Name))
                            sql += exp.Method.Name;
                        foreach (var arg in exp.Arguments)
                        {
                            VisitExpression(arg);
                        }
                        sql += " and ";
                    }
                    break;
                case ExpressionType.Quote:
                    UnaryExpression expUnary = expression as UnaryExpression;
                    if (expUnary != null)
                    {
                        VisitExpression(expUnary.Operand);
                    }
                    break;
                case ExpressionType.Lambda:
                    LambdaExpression expLambda = expression as LambdaExpression;
                    if (expLambda != null)
                    {
                        VisitExpression(expLambda.Body);
                    }
                    break;
                case ExpressionType.Equal:
                    BinaryExpression expBinary = expression as BinaryExpression;
                    if (expBinary != null)
                    {
                        var left = expBinary.Left;
                        var right = expBinary.Right;
                        sql += " " + left.ToString().Split('.')[1] + " = '" + right.ToString().Replace("\"", "") + "'";
                    }
                    break;
                case ExpressionType.Constant:
                    break;
                default:
                    throw new NotSupportedException(string.Format("This kind of expression is not supported, {0}", expression.NodeType));
            }
        }

        public static void GenerateSelectHeader<T>()
        {
            var type = typeof(T);
            if (type.IsGenericType)
                type = type.GetGenericArguments()[0];
            var typeName = type.Name.Replace("\"", "");
            sql = string.Format("select * from {0} ", typeName);
        }
    }
}
