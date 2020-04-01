using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class AbstractValidateAttribute : Attribute
    {
        public abstract bool Validate(object value);
    }
}
