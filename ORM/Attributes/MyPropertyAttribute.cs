using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MyPropertyAttribute : BaseAttribute
    {
        public MyPropertyAttribute(string name) : base(name)
        {
        }
    }
}
