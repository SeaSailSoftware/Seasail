using SQLite.CodeFirst;
using System.Data.Entity;

namespace Seasail.DataAccess.EF
{
    internal class SQLiteInitializer : SqliteCreateDatabaseIfNotExists<SQLiteDbContext>
    {
        public SQLiteInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder, true)
        {
        }

        protected override void Seed(SQLiteDbContext context)
        {
            base.Seed(context);

        }
    }
}
