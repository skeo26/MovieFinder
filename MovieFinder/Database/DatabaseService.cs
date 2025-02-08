using BLMovieFinder.Models;
using Microsoft.Extensions.DependencyInjection;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieFinder.Database
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _db;
        private readonly IDataSeeder _dataSeeder;

        public DatabaseService(SQLiteAsyncConnection db, IDataSeeder dataSeeder)
        {
            _db = db;
            _dataSeeder = dataSeeder;
        }

        public async Task InitAsync()
        {
            await _db.CreateTableAsync<Movie>();
            await _db.CreateTableAsync<Actor>();
            await _db.CreateTableAsync<MovieActor>();

            await _dataSeeder.SeedAsync(_db);
        }
    }
}