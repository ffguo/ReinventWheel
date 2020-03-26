using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace ORM.Expressions
{
    /// <summary>
    /// sql表达式树解析目录
    /// </summary>
    public class SqlVistor : ExpressionVisitor
    {
        private List<Stack<string>> nodes = new List<Stack<string>>();
        private Stack<string> currentNode = new Stack<string>();
        private string currentVariableName = string.Empty;

        public string GetWhereSql()
        {
            return " where " + GetSql();
        }

        public string GetSql()
        {
            return string.Join(" and ", nodes.Select(a => string.Join(" ", a.ToArray())));
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            currentNode = new Stack<string>();

            var right = node.Right;
            Visit(right);

            currentNode.Push(ConvertCommparison(node.NodeType));

            var left = node.Left;
            Visit(left);

            nodes.Add(currentNode);
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression.NodeType == ExpressionType.Constant)
            {
                currentVariableName = node.Member.Name;
                Visit(node.Expression);
            }
            else
                currentNode.Push(node.Member.Name);
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            string value = string.Empty;
            if(node.Value.GetType().IsClass && node.Value.GetType() != typeof(string))
            {
                var filds = node.Value.GetType().GetFields();
                if (filds.Length > 0 && filds.Any(a => a.Name == currentVariableName))
                {
                    value = ConvertSqlValue(filds.FirstOrDefault(a => a.Name == currentVariableName).GetValue(node.Value));
                }
            }
            else
            {
                value = ConvertSqlValue(node.Value);
            }
            currentNode.Push(value);
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

            if (valueType == typeof(string))
                value = $"'{value}'";

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
