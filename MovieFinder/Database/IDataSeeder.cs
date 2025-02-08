
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFinder.Database
{
    public interface IDataSeeder
    {
        Task SeedAsync(SQLiteAsyncConnection db);
    }
}
