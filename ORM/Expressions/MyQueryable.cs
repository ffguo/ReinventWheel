using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Expressions
{
    public class MyQueryable<T> : IQueryable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            Console.WriteLine("Begin to iterate.");
            var myProvider = Provider as IMyQueryProvider;
            var result = myProvider.Executes<T>(Expression);
            foreach (var item in result)
            {
                Console.WriteLine(item);
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression { get; private set; }
        public Type ElementType { get; private set; }
        public IQueryProvider Provider { get; private set; }

        public MyQueryable() : this(new MyQueryProvider(), null)
        {
            //this is T
            Expression = Expression.Constant(this);
        }

        public MyQueryable(MyQueryProvider provider, Expression expression)
        {
            Expression = expression;
            ElementType = typeof(T);
            Provider = provider;
        }
    }
}
