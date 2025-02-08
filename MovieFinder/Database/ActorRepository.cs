
using BLMovieFinder;
using BLMovieFinder.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFinder.Database
{
    public class ActorRepository : IActorRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public ActorRepository(SQLiteAsyncConnection db)
        {
            _db = db;
        }

        public Task<int> AddActorAsync(Actor actor)
        {
            return _db.InsertAsync(actor);
        }

        public Task<int> DeleteAsync(int id)
        {
            return _db.DeleteAsync(id);
        }

        public Task<List<Actor>> GetActorsAsync()
        {
            return _db.Table<Actor>().ToListAsync();
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            return await _db.Table<Actor>().FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
