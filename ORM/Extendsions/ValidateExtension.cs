using ORM.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ORM.Extendsions
{
    public static class ValidateExtension
    {
        public static bool Validate<T>(this T entity)
        {
            var type = typeof(T);
            foreach (var prop in type.GetProperties())
            {
                var value = prop.GetValue(entity);
                if(prop.IsDefined(typeof(AbstractValidateAttribute), true))
                {
                    var attrs = prop.GetCustomAttributes<AbstractValidateAttribute>();
                    foreach (var attr in attrs)
                    {
                        if (!attr.Validate(value))
                            return false;
                    }
                }
            }
            return true;
        }
    }
}
