using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Attributes
{
    public class RequiredValidateAttribute : AbstractValidateAttribute
    {
        public override bool Validate(object value)
        {
            return value != null;
        }
    }
}
