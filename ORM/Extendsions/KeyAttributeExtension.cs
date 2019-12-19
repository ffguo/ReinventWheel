using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using ORM.Attributes;

namespace ORM.Extendsions
{
    /// <summary>
    /// 主键特性扩展方法
    /// </summary>
    public static class KeyAttributeExtension
    {
        public static string GetKeyName(this Type type)
        {
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (prop.IsDefined(typeof(MyKeyAttribute), true))
                {
                    return prop.Name;
                }
            }
            return "Id";
        }

        public static string GetName<T>(this MemberInfo info)
            where T : BaseAttribute
        {
            if (info.IsDefined(typeof(T), true))
            {
                var attr = info.GetCustomAttribute<T>();
                return attr.GetName();
            }
            return info.Name;
        }

        public static IEnumerable<PropertyInfo> NoKey(this PropertyInfo[] infos)
        {
            return infos.Where(a => !a.IsDefined(typeof(MyKeyAttribute), true));
        }
    }
}
