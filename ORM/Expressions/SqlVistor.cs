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

            if (node.NodeType == ExpressionType.Equal)
            {
                nodes.Push("=");
            }

            var left = node.Left;
            Visit(left);
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            nodes.Push(node.Member.Name);
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            nodes.Push(node.Value.ToString());
            return node;
        }
    }
}
