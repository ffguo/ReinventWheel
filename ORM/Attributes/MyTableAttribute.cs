using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MyTableAttribute : BaseAttribute
    {
        public MyTableAttribute(string name) : base(name)
        {
        }
    }
}
