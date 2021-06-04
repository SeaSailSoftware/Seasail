using Seasail;
using Seasail.DataAccess;
using Seasail.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.DataAccess
{
    public static class DatabaseFactory
    {
        public static IDatabase CreateInstance()
        {
            IDatabase database = null;
            string dbType = GlobalContext.DBProvider;
            string dbConnectionString = GlobalContext.DBConnectionString;
            string assemblyName = GlobalContext.AssemblyName;
            switch (dbType)
            {
                case "SqlServer":
                    DbHelper.DbType = DatabaseType.SqlServer;
                    database = new SqlServerDatabase(dbConnectionString, assemblyName);
                    break;
                case "SQLite":
                    DbHelper.DbType = DatabaseType.SQLite;
                    database = new SQLiteDatabase(dbConnectionString, assemblyName);
                    break;
                default:
                    throw new Exception("未找到数据库配置");
            }
            return database;
        }
    }
}
