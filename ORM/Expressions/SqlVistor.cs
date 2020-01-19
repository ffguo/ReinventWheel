using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace ORM.Expressions
{
    /// <summary>
    /// sql表达式树解析目录
    /// </summary>
    public class SqlVistor : ExpressionVisitor
    {
        private Stack<string> nodes = new Stack<string>();

        public string GetSql()
        {
            return string.Join(" ", nodes.ToArray());
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var right = node.Right;
            Visit(right);

            nodes.Push(ConvertCommparison(node.NodeType));

            var left = node.Left;
            Visit(left);
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression.NodeType == ExpressionType.Constant)
                Visit(node.Expression);
            else
                nodes.Push(node.Member.Name);
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            string value = string.Empty;
            if(node.Value.GetType().IsClass)
            {
                var filds = node.Value.GetType().GetFields();
                if (filds.Length > 0)
                {
                    value = ConvertSqlValue(filds[0].GetValue(node.Value));
                }
            }
            else
            {
                value = ConvertSqlValue(node.Value);
            }
            nodes.Push(value);
            return node;
        }

        private string ConvertSqlValue(object obj)
        {
            if (obj == null)
                return string.Empty;

            string value = obj.ToString();
            var valueType = obj.GetType();
            if (valueType == typeof(bool) || 
                valueType == typeof(bool?))
            {
                value = Convert.ToBoolean(obj) ? "1" : "0";
            }
            return value;
        }

        private string ConvertCommparison(ExpressionType nodeType)
        {
            var commparison = string.Empty;
            switch (nodeType)
            {
                case ExpressionType.Equal:
                    commparison = "=";
                    break;
                case ExpressionType.GreaterThan:
                    commparison = ">";
                    break;
                case ExpressionType.LessThan:
                    commparison = "<";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    commparison = ">=";
                    break;
                case ExpressionType.LessThanOrEqual:
                    commparison = "<=";
                    break;
                case ExpressionType.AndAlso:
                    commparison = "and";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("不明的对比类型");
            }

            return commparison;
        }
    }
}
