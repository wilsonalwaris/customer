using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public static class SqliteHelper
    {
        public static DbContextOptions<CustomerContext> GetInMemoryDatabaseOptions()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());
            return new DbContextOptionsBuilder<CustomerContext>()
                          .UseSqlite(connection)
                          .Options;
        }

        public static DbContextOptions<CustomerContext> GetDatabaseOptions()
        {
            ////var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "/customerDb.db" };
            var connection = new SqliteConnection("Data Source=customerDb.db;");
            return new DbContextOptionsBuilder<CustomerContext>()
                          .UseSqlite(connection)
                          .Options;
        }

        public static bool DatabaseDoesNotExist(CustomerContext customerContext)
        {
            if (customerContext.Database.CanConnect())
            {
                customerContext.Database.OpenConnection();
                return false;
            }

            customerContext.Database.OpenConnection();
            return !customerContext.Database.EnsureCreated();
        }

        public static bool InMemoryDatabaseDoesNotExist(CustomerContext customerContext)
        {
            customerContext.Database.OpenConnection();
            return !customerContext.Database.EnsureCreated();
        }
    }
}
