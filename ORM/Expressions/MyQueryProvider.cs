using ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Expressions
{
    public class MyQueryProvider : IMyQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            Console.WriteLine("Going to CreateQuery");
            return new MyQueryable<TElement>(this, expression);
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            dynamic result = "123";
            return (TResult)result;
        }

        public List<TResult> Executes<TResult>(Expression expression)
        {

            //string sql = ExpressionTreeToSql.GetSql<TResult>(expression);
            var sql = SqlBulider<TResult>.GetSql(SqlType.Select);
            var vistor = new SqlVistor();
            vistor.Visit(expression);
            sql += vistor.GetWhereSql();

            Console.WriteLine(sql);

            var result = new MyORM<TResult>().GetList(sql);
            return result;
        }


    }
}
