using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using ORM.Model;
using ORM.Extendsions;
using ORM.Attributes;
using ORM.Expressions;
using System.Linq.Expressions;

namespace ORM
{
    /// <summary>
    /// 自定义ORM
    /// </summary>
    public class MyORM<T>
        where T : BaseEntity, new()
    {
        private readonly string _connStr = "server=DESKTOP-UPKUKII;database=Write;uid=sa;password=sasasa";

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Find(int id)
        {
            var type = typeof(T);
            var sql = SqlBulider<T>.GetSql(SqlType.Find);
            var parameters = new List<SqlParameter>() { new SqlParameter($"@{SqlBulider<T>.KeyName}", id) };
            return ExecuteSql(sql, parameters.ToArray(), command =>
            {
                var reader = command.ExecuteReader();
                return ConvertList(reader).FirstOrDefault();
            });

            #region 封装前
            //using (SqlConnection conn = new SqlConnection(_connStr))
            //{
            //    conn.Open();
            //    var command = new SqlCommand(sql, conn);
            //    command.Parameters.Add(new SqlParameter($"@{SqlBulider<T>.KeyName}", id));
            //    var reader = command.ExecuteReader();
            //    if (reader.Read())
            //    {
            //        var entity = new T();
            //        var props = type.GetProperties();
            //        foreach (var prop in props)
            //        {
            //            prop.SetValue(entity, reader[prop.Name]);
            //        }
            //        return entity;
            //    }
            //}
            //return default; 
            #endregion
        }

        /// <summary>
        /// 过滤查找
        /// </summary>
        /// <param name="whereExp"></param>
        /// <returns></returns>

        public List<T> Where(Expression<Func<T, bool>> whereExp)
        {
            var vistor = new SqlVistor();
            vistor.Visit(whereExp);
            var whereSql = vistor.GetSql();

            var type = typeof(T);
            var sql = SqlBulider<T>.GetSql(SqlType.Select, whereSql);
            return ExecuteSql(sql, null, command =>
            {
                var reader = command.ExecuteReader();
                return ConvertList(reader);
            });
        }

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(T entity)
        {
            var type = typeof(T);
            var props = type.GetProperties().NoKey();
            var sql = SqlBulider<T>.GetSql(SqlType.Insert);
            var parameters = props.Select(a => new SqlParameter($"@{a.GetName<MyPropertyAttribute>()}", a.GetValue(entity) ?? DBNull.Value)).ToArray();
            return ExecuteSql<bool>(sql, parameters, command =>
            {
                return command.ExecuteNonQuery() > 0;
            });

            #region 封装前
            //using (SqlConnection conn = new SqlConnection(_connStr))
            //{
            //    conn.Open();
            //    var command = new SqlCommand(sql, conn);
            //    foreach (var prop in props)
            //    {
            //        var name = prop.GetName<MyPropertyAttribute>();
            //        command.Parameters.Add(new SqlParameter($"@{name}", prop.GetValue(entity)));
            //    }
            //    isSuccess = command.ExecuteNonQuery() > 0;
            //}
            //return isSuccess; 
            #endregion
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            var type = typeof(T);
            var props = type.GetProperties();
            var sql = SqlBulider<T>.GetSql(SqlType.Update);
            var parameters = props.Select(a => new SqlParameter($"@{a.GetName<MyPropertyAttribute>()}", a.GetValue(entity) ?? DBNull.Value)).ToArray();
            return ExecuteSql(sql, parameters, command =>
            {
                return command.ExecuteNonQuery() > 0;
            });
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var type = typeof(T);
            var sql = SqlBulider<T>.GetSql(SqlType.Delete);
            var parameters = new List<SqlParameter>() { new SqlParameter($"@{SqlBulider<T>.KeyName}", id) };
            return ExecuteSql(sql, parameters.ToArray(), command =>
            {
                return command.ExecuteNonQuery() > 0;
            });
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateCondition(T entity, Expression<Func<T, bool>> whereExp)
        {
            var vistor = new SqlVistor();
            vistor.Visit(whereExp);
            var whereSql = vistor.GetSql();

            var type = typeof(T);
            var props = type.GetProperties();
            var sql = $"UPDATE [{type.GetName<MyTableAttribute>()}] SET {string.Join(",", props.NoKey().Select(a => $"{a.Name} = @{a.Name}"))} WHERE {whereSql}";
            var parameters = props.Select(a => new SqlParameter($"@{a.GetName<MyPropertyAttribute>()}", a.GetValue(entity) ?? DBNull.Value)).ToArray();
            return true; //ExecuteSql(sql, parameters, command =>
            //{
            //    return command.ExecuteNonQuery() > 0;
            //});
        }

        private TR ExecuteSql<TR>(string sql, SqlParameter[] sqlParameters, Func<SqlCommand, TR> action)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                var command = new SqlCommand(sql, conn);
                if (sqlParameters!=null)
                    command.Parameters.AddRange(sqlParameters);
                return action.Invoke(command);
            }
        }

        private List<T> ConvertList(SqlDataReader reader)
        {
            var type = typeof(T);
            var list = new List<T>();
            while (reader.Read())
            {
                var entity = new T();
                var props = type.GetProperties();
                foreach (var prop in props)
                {
                    var value = reader[prop.Name];
                    prop.SetValue(entity, value == DBNull.Value ? null : value);
                }
                list.Add(entity);
            }
            return list;
        }
    }
}
