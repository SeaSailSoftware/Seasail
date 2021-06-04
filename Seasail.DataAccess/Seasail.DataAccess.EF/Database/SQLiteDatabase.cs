using Seasail.DataAccess.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Seasail.DataAccess.EF
{
    public class SQLiteDatabase : IDatabase
    {
        #region 构造函数
        /// <summary>
        /// 构造方法
        /// </summary>
        public SQLiteDatabase(string connString,string assemblyName)
        {
            dbContext = new SQLiteDbContext(connString,assemblyName);
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        public DbContext dbContext { get; set; }
        /// <summary>
        /// 事务对象
        /// </summary>
        public DbContextTransaction dbContextTransaction { get; set; }
        #endregion

        #region 事务提交
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        public async Task<IDatabase> BeginTrans()
        {
            DbConnection dbConnection = dbContext.Database.Connection;
            if (dbConnection.State == ConnectionState.Closed)
            {
                await dbConnection.OpenAsync();
            }
            dbContextTransaction = await Task.Run(dbContext.Database.BeginTransaction);
            return this;
        }
        /// <summary>
        /// 提交当前操作的结果
        /// </summary>
        public async Task<int> CommitTrans()
        {
            try
            {
                DbContextExtension.SetEntityDefaultValue(dbContext);
                int returnValue = await dbContext.SaveChangesAsync();
                if (dbContextTransaction != null)
                {
                    await Task.Run(dbContextTransaction.Commit);
                    await this.Close();
                }
                else
                {
                    await this.Close();
                }
                return returnValue;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dbContextTransaction == null)
                {
                    await this.Close();
                }
            }
        }
        /// <summary>
        /// 把当前操作回滚成未提交状态
        /// </summary>
        public async Task RollbackTrans()
        {
            await Task.Run(this.dbContextTransaction.Rollback);
            await Task.Run(this.dbContextTransaction.Dispose);
            await this.Close();
        }
        /// <summary>
        /// 关闭连接 内存回收
        /// </summary>
        public async Task Close()
        {
            await Task.Run(dbContext.Dispose);
        }

        #endregion

        #region 执行SQL

        public async Task<int> ExecuteBySql(string strSql)
        {
            return await dbContext.Database.ExecuteSqlCommandAsync(strSql);
        }

        public async Task<int> ExecuteBySql(string strSql, params DbParameter[] dbParameter)
        {
            return await dbContext.Database.ExecuteSqlCommandAsync(strSql, dbParameter);
        }


        public async Task<int> Insert<T>(T entity) where T : class
        {
            dbContext.Entry(entity).State = EntityState.Added;
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> Insert<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                dbContext.Entry<T>(entity).State = EntityState.Added;
            }
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete<T>() where T : class
        {
            var tableName = DbContextExtension.GetTableName<T>(dbContext);
            if (!string.IsNullOrEmpty(tableName))
                return await this.ExecuteBySql(DbContextExtension.DeleteSql(tableName));
            return -1;
        }


        public async Task<int> Delete<T>(T entity) where T : class
        {
            dbContext.Set<T>().Attach(entity);
            dbContext.Set<T>().Remove(entity);
            return dbContextTransaction == null ? await this.CommitTrans() : 0;
        }

        public async Task<int> Delete<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                dbContext.Set<T>().Attach(entity);
                dbContext.Set<T>().Remove(entity);
            }
            return dbContextTransaction == null ? await this.CommitTrans() : 0;
        }

        public async Task<int> Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            IEnumerable<T> entities = await dbContext.Set<T>().Where(condition).ToListAsync();
            return entities.Count() > 0 ? await Delete(entities) : 0;
        }

        public async Task<int> DeleteById<T>(dynamic id) where T : class
        {
            string tableName = DbContextExtension.GetTableName<T>(dbContext);
            string keyField = "Id";
            return await this.ExecuteBySql(DbContextExtension.DeleteSql(tableName, keyField, id));
        }

        public async Task<int> DeleteByIds<T>(dynamic[] ids) where T : class
        {
            string tableName = DbContextExtension.GetTableName<T>(dbContext);
            string keyField = "Id";
            return await this.ExecuteBySql(DbContextExtension.DeleteSql(tableName, keyField, ids));
        }

        public async Task<int> Delete<T>(string propertyName, dynamic propertyValue) where T : class
        {
            string tableName = DbContextExtension.GetTableName<T>(dbContext);
            string keyField = propertyName;
            return await this.ExecuteBySql(DbContextExtension.DeleteSql(tableName, keyField, propertyValue));
        }

        public async Task<int> Update<T>(T entity) where T : class
        {
            dbContext.Set<T>().Attach(entity);
            Hashtable props = DatabasesExtension.GetPropertyInfo<T>(entity);
            foreach (string item in props.Keys)
            {
                if (item == "Id")
                {
                    continue;
                }
                object value = dbContext.Entry(entity).Property(item).CurrentValue;

                if (value != null)
                {
                    dbContext.Entry(entity).Property(item).IsModified = true;
                }
            }
            return dbContextTransaction == null ? await this.CommitTrans() : 0;
        }

        public async Task<int> Update<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                await this.Update(entity);
            }
            return dbContextTransaction == null ? await this.CommitTrans() : 0;
        }

        public async Task<int> UpdateAllField<T>(T entity) where T : class
        {
            dbContext.Set<T>().Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContextTransaction == null ? await this.CommitTrans() : 0;
        }

        public async Task<int> Update<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            IEnumerable<T> entities = await dbContext.Set<T>().Where(condition).ToListAsync();
            return entities.Count() > 0 ? await Update(entities) : 0;
        }

        public IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return dbContext.Set<T>().Where(condition);
        }

        public async Task<T> FindEntity<T>(object keyValue) where T : class
        {
            return await dbContext.Set<T>().FindAsync(keyValue);
        }

        public async Task<T> FindEntity<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await dbContext.Set<T>().Where(condition).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindList<T>() where T : class, new()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindList<T>(Func<T, object> orderby) where T : class, new()
        {
            var list = await dbContext.Set<T>().ToListAsync();
            list = list.OrderBy(orderby).ToList();
            return list;
        }

        public async Task<IEnumerable<T>> FindList<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await dbContext.Set<T>().Where(condition).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindList<T>(string strSql) where T : class
        {
            return await FindList<T>(strSql, null);
        }

        public async Task<IEnumerable<T>> FindList<T>(string strSql, DbParameter[] dbParameter) where T : class
        {
            var dbConnection = dbContext.Database.Connection;
            var IDataReader = await new DbHelper(dbConnection).ExecuteReadeAsync(CommandType.Text, strSql, dbParameter);
            return DatabasesExtension.IDataReaderToList<T>(IDataReader);
        }

        public async Task<(int total, IEnumerable<T> list)> FindList<T>(string orderField, bool isAsc, int pageSize, int pageIndex) where T : class, new()
        {
            string[] _order = orderField.Split(',');
            var tempData = dbContext.Set<T>().AsQueryable();
            return await FindList<T>(tempData, orderField, isAsc, pageSize, pageIndex);
        }

        public async Task<(int total, IEnumerable<T> list)> FindList<T>(Expression<Func<T, bool>> condition, string orderField, bool isAsc, int pageSize, int pageIndex) where T : class, new()
        {
            string[] _order = orderField.Split(',');
            var tempData = dbContext.Set<T>().Where(condition);
            return await FindList<T>(tempData, orderField, isAsc, pageSize, pageIndex);
        }

        public async Task<(int total, IEnumerable<T>)> FindList<T>(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex) where T : class
        {
            return await FindList<T>(strSql, null, orderField, isAsc, pageSize, pageIndex);
        }

        public async Task<(int total, IEnumerable<T>)> FindList<T>(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex) where T : class
        {
            var dbConnection = dbContext.Database.Connection;
            StringBuilder sb = new StringBuilder();
            sb.Append(new DatabasePageExtension().SqlPageSql(strSql, dbParameter, orderField, isAsc, pageSize, pageIndex));
            object tempTotal = await new DbHelper(dbConnection).ExecuteScalarAsync(CommandType.Text, "Select Count(1) From (" + strSql + ")  t", dbParameter);
            int total = Convert.ToInt32(tempTotal);
            if (total > 0)
            {
                var IDataReader = await new DbHelper(dbConnection).ExecuteReadeAsync(CommandType.Text, sb.ToString(), dbParameter);
                return (total, DatabasesExtension.IDataReaderToList<T>(IDataReader));
            }
            else
            {
                return (total, new List<T>());
            }
        }

        public async Task<DataTable> FindTable(string strSql)
        {
            return await FindTable(strSql, null);
        }

        public async Task<DataTable> FindTable(string strSql, DbParameter[] dbParameter)
        {
            var dbConnection = dbContext.Database.Connection;
            var IDataReader = await new DbHelper(dbConnection).ExecuteReadeAsync(CommandType.Text, strSql, dbParameter);
            return DatabasesExtension.IDataReaderToDataTable(IDataReader);
        }

        public async Task<(int total, DataTable)> FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            return await FindTable(strSql, null, orderField, isAsc, pageSize, pageIndex);
        }

        public async Task<(int total, DataTable)> FindTable(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            var dbConnection = dbContext.Database.Connection;
            StringBuilder sb = new StringBuilder();
            sb.Append(new DatabasePageExtension().SqlPageSql(strSql, dbParameter, orderField, isAsc, pageSize, pageIndex));
            object tempTotal = await new DbHelper(dbConnection).ExecuteScalarAsync(CommandType.Text, "SELECT COUNT(1) FROM (" + strSql + ") t ", dbParameter);
            int total = Convert.ToInt32(tempTotal);
            var IDataReader = await new DbHelper(dbConnection).ExecuteReadeAsync(CommandType.Text, sb.ToString(), dbParameter);
            DataTable resultTable = DatabasesExtension.IDataReaderToDataTable(IDataReader);
            return (total, resultTable);
        }

        public async Task<object> FindObject(string strSql)
        {
            return await FindObject(strSql, null);
        }

        public async Task<object> FindObject(string strSql, DbParameter[] dbParameter)
        {
            var dbConnection = dbContext.Database.Connection;
            return await new DbHelper(dbConnection).ExecuteScalarAsync(CommandType.Text, strSql, dbParameter);
        }

        public async Task<T> FindObject<T>(string strSql) where T : class
        {
            var list = await dbContext.SqlQuery<T>(strSql);
            return list.FirstOrDefault();
        }
        #endregion


        private async Task<(int total, IEnumerable<T> list)> FindList<T>(IQueryable<T> tempData, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            string[] _order = orderField.Split(',');
            MethodCallExpression resultExp = null;
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(T), "t");
                var property = typeof(T).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(T), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<T>(resultExp);
            var total = tempData.Count();
            tempData = tempData.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable();
            var list = await tempData.ToListAsync();
            return (total, list);
        }
    }
}
