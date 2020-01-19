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
        private static readonly string _selectSql;

        static SqlBulider()
        {
            var type = typeof(T);
            var exceptKeyCloumns = type.GetProperties().NoKey();
            KeyName = type.GetKeyName();
            _selectSql = $"SELECT {string.Join(",", type.GetProperties().Select(a => a.GetName<MyPropertyAttribute>()))} FROM {type.GetName<MyTableAttribute>()}";
            _findSql = $"{_selectSql} WHERE {KeyName}=@{KeyName}";
            _insertSql = $"INSERT INTO [{type.GetName<MyTableAttribute>()}]({string.Join(",", exceptKeyCloumns.Select(a => a.Name))}) VALUES({string.Join(",", exceptKeyCloumns.Select(a => "@" + a.Name))})";
            _updateSql = $"UPDATE [{type.GetName<MyTableAttribute>()}] SET {string.Join(",", exceptKeyCloumns.Select(a =>$"{a.Name} = @{a.Name}"))} WHERE {KeyName}=@{KeyName}";
            _deleteSql = $"DELETE FROM [{type.GetName<MyTableAttribute>()}] WHERE {KeyName}=@{KeyName}";
        }

        public static string GetSql(SqlType sqlType, string where = null)
        {
            string sql = string.Empty;
            if (sqlType == SqlType.Find)
                sql = _findSql;
            else if (sqlType == SqlType.Insert)
                sql = _insertSql;
            else if (sqlType == SqlType.Update)
                sql = _updateSql;
            else if (sqlType == SqlType.Delete)
                sql = _deleteSql;
            else if (sqlType == SqlType.Select)
                sql = _selectSql;

            if (!string.IsNullOrEmpty(where))
                sql += $" WHERE {where}";

            return sql;
        }
    }
}
