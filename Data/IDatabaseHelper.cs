using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public interface IDatabaseHelper
    {
        DbContextOptions<CustomerContext> GetDatabaseOptions();

        SqliteConnection GetConnection();
    }
}