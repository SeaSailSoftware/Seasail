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
using System.Threading.Tasks;

namespace Seasail.DataAccess.EF
{
    public class SqlServerDatabase : IDatabase
    {
        #region 构造函数
        /// <summary>
        /// 构造方法
        /// </summary>
        public SqlServerDatabase(string connString,string assemblyName)
        {
            dbContext = new SqlServerDbContext(connString,assemblyName);
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
                //DbContextExtension.SetEntityDefaultValue(dbContext);

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
            return await dbContext.Database.ExecuteSqlCommandAsync(strSql,dbParameter);
        }


        public async Task<int> Insert<T>(T entity) where T : class
        {
            dbContext.Set<T>().Add(entity);
            return await dbContext.SaveChangesAsync();
        }

        public Task<int> Insert<T>(IEnumerable<T> entities) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(IEnumerable<T> entities) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteById<T>(dynamic id) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteByIds<T>(dynamic[] id) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(string propertyName, dynamic propertyValue) where T : class
        {
            throw new NotImplementedException();
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

        public Task<int> Update<T>(IEnumerable<T> entities) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAllField<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> Update<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<T> FindEntity<T>(object KeyValue) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindEntity<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await dbContext.Set<T>().Where(condition).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindList<T>() where T : class, new()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public Task<IEnumerable<T>> FindList<T>(Func<T, object> orderby) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> FindList<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await dbContext.Set<T>().Where(condition).ToListAsync();
        }

        public Task<IEnumerable<T>> FindList<T>(string strSql) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindList<T>(string strSql, DbParameter[] dbParameter) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<(int total, IEnumerable<T> list)> FindList<T>(string orderField, bool isAsc, int pageSize, int pageIndex) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<(int total, IEnumerable<T> list)> FindList<T>(Expression<Func<T, bool>> condition, string orderField, bool isAsc, int pageSize, int pageIndex) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<(int total, IEnumerable<T>)> FindList<T>(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<(int total, IEnumerable<T>)> FindList<T>(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> FindTable(string strSql)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> FindTable(string strSql, DbParameter[] dbParameter)
        {
            throw new NotImplementedException();
        }

        public Task<(int total, DataTable)> FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public Task<(int total, DataTable)> FindTable(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public Task<object> FindObject(string strSql)
        {
            throw new NotImplementedException();
        }

        public Task<object> FindObject(string strSql, DbParameter[] dbParameter)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindObject<T>(string strSql) where T : class
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
