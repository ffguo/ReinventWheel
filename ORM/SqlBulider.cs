using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ORM.Extendsions;
using ORM.Attributes;

namespace ORM
{
    /// <summary>
    /// SQL生成器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SqlBulider<T>
    {
        public static readonly string KeyName;
        private static readonly string _insertSql;
        private static readonly string _findSql;
        private static readonly string _updateSql;
        private static readonly string _deleteSql;

        static SqlBulider()
        {
            var type = typeof(T);
            var exceptKeyCloumns = type.GetProperties().NoKey();
            KeyName = type.GetKeyName();
            _findSql = $"SELECT {string.Join(",", type.GetProperties().Select(a => a.GetName<MyPropertyAttribute>()))} FROM {type.GetName<MyTableAttribute>()} WHERE {KeyName}=@{KeyName}";
            _insertSql = $"INSERT INTO [{type.GetName<MyTableAttribute>()}]({string.Join(",", exceptKeyCloumns.Select(a => a.Name))}) VALUES({string.Join(",", exceptKeyCloumns.Select(a => "@" + a.Name))})";
            _updateSql = $"UPDATE [{type.GetName<MyTableAttribute>()}] SET {string.Join(",", exceptKeyCloumns.Select(a =>$"{a.Name} = @{a.Name}"))} WHERE {KeyName}=@{KeyName}";
            _deleteSql = $"DELETE FROM [{type.GetName<MyTableAttribute>()}] WHERE {KeyName}=@{KeyName}";
        }

        public static string GetSql(SqlType sqlType)
        {
            if (sqlType == SqlType.Find)
                return _findSql;
            else if (sqlType == SqlType.Insert)
                return _insertSql;
            else if (sqlType == SqlType.Update)
                return _updateSql;
            else if (sqlType == SqlType.Delete)
                return _deleteSql;

            return string.Empty;
        }
    }
}
