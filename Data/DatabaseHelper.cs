using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Data
{
    public class DatabaseHelper : IDatabaseHelper
    {
        public DbContextOptions<CustomerContext> GetDatabaseOptions()
        {
            return new DbContextOptionsBuilder<CustomerContext>()
                          .UseSqlite(this.GetConnection())
                          .Options;
        }

        public SqliteConnection GetConnection()
        {
            var builder = new SqliteConnectionStringBuilder();
            builder.DataSource = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, builder.DataSource));
            var connectionString = builder.ToString();

            return new SqliteConnection(connectionString + "/customerDb.db");
        }
    }
}
