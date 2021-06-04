using Seasail.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seasail.DataAccess
{
    public class RepositoryFactory
    {
        public Repository BaseRepository()
        {
            IDatabase database = null;
            string dbType = GlobalContext.DBProvider;
            string assemblyName = GlobalContext.EntityAssembly;
            switch (dbType)
            {
                case "SqlServer":
                    DbHelper.DbType = DatabaseType.SqlServer;
                    database = new SqlServerDatabase(GlobalContext.DBConnectionString, assemblyName);
                    break;
                case "SQLite":
                    DbHelper.DbType = DatabaseType.SQLite;
                    database = new SQLiteDatabase(GlobalContext.DBConnectionString, assemblyName);
                    break;
                default:
                    throw new Exception("未找到数据库配置");
            }
            return new Repository(database);
        }
    }
}
