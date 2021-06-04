using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Seasail.DataAccess.EF
{
    public class SQLiteDbContext : DbContext, IDisposable
    {
        private string ConnectionString { get; set; }

        private string _assemblyName;
        #region 构造函数

        public SQLiteDbContext(string connectionString,string assemblyName) : base(new SQLiteConnection(connectionString), true)
        {
            ConnectionString = connectionString;
            _assemblyName = assemblyName;
        }
        #endregion

        #region 重载
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            System.Data.Entity.Database.SetInitializer<SQLiteDbContext>(new SQLiteInitializer(modelBuilder));
            Assembly entityAssembly = Assembly.Load(new AssemblyName(_assemblyName));
            IEnumerable<Type> typesToRegister = entityAssembly.GetTypes().Where(p => !string.IsNullOrEmpty(p.Namespace))
                                                                         .Where(p => !string.IsNullOrEmpty(p.GetCustomAttribute<TableAttribute>()?.Name));
            foreach (Type type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.RegisterEntityType(type);
            }
            //foreach (var entity in modelBuilder.Entity().)
            //{
            //    PrimaryKeyConvention.SetPrimaryKey(modelBuilder, entity.Name);
            //    var currentTableName = modelBuilder.Entity(entity.Name).Metadata.GetTableName();
            //    modelBuilder.Entity(entity.Name).ToTable(currentTableName.ToLower());

            //    var properties = entity.GetProperties();
            //    foreach (var property in properties)
            //    {
            //        ColumnConvention.SetColumnName(modelBuilder, entity.Name, property.Name);
            //    }
            //}

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
