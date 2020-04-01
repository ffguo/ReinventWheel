using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ORM.Attributes
{
    public class LengthValidateAttribute : AbstractValidateAttribute
    {
        private readonly int min;
        private readonly int max;

        public LengthValidateAttribute(int max) : this(0, max)
        {
            
        }

        public LengthValidateAttribute(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public override bool Validate(object value)
        {
            var length = value.ToString().Length;
            return (length >= min && length <= max);
        }
    }
}
