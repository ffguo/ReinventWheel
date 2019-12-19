using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Attributes
{
    public abstract class BaseAttribute: Attribute
    {
        private readonly string name;
        public BaseAttribute(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }
    }
}
