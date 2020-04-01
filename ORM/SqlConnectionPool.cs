using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ORM
{
    public class SqlConnectionPool
    {
        private static int _send = 0;
        private static string _writeConnStr;
        private static List<string> _readConnStrs;

        public static void Init(string writeConnStr, List<string> readConnStrs)
        {
            _writeConnStr = writeConnStr;
            _readConnStrs = readConnStrs;
        }

        public static string GetConnectionString(SqlConnectionType sqlConnectionType)
        {
            string connStr = null;
            switch(sqlConnectionType)
            {
                case SqlConnectionType.Write:
                    connStr = _writeConnStr;
                    break;
                case SqlConnectionType.Read:
                    connStr = GetSqlConnectionsStringStrategy(_readConnStrs);
                    break;
                default:
                    throw new Exception("未知的数据库连接类型");
            }
            return connStr;
        }

        private static string GetSqlConnectionsStringStrategy(List<string> connStrs, StrategyType strategyType = StrategyType.Average)
        {
            string connStr = null;
            switch(strategyType)
            {
                case StrategyType.Average:
                    connStr = connStrs[new Random(_send++).Next(0, connStrs.Count)];
                    break;
                case StrategyType.Polling:
                    connStr = connStrs[_send++ % connStrs.Count];
                    break;
                default:
                    throw new Exception("未知的策略类型");
            }
            return connStr;
        }
    }

    public enum StrategyType
    {
        // 轮询
        Polling,
        // 平均
        Average,
        // 权重
        Weight
    }

    public enum SqlConnectionType
    {
        Write,
        Read
    }
}
