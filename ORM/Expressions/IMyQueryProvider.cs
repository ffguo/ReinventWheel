using ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Expressions
{
    public interface IMyQueryProvider : IQueryProvider
    {
        List<TResult> Executes<TResult>(Expression expression);
    }
}
