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
    public class MovieActorRepository : IMovieActorRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public MovieActorRepository(SQLiteAsyncConnection db)
        {
            _db = db;
        }

        public Task<int> AddAsync(MovieActor movieActor)
        {
            return _db.InsertAsync(movieActor);
        }

        public Task<int> DeleteByActorIdAsync(int actorId)
        {
            return _db.Table<MovieActor>().Where(ma => ma.ActorId == actorId).DeleteAsync();
        }

        public Task<int> DeleteByMovieIdAsync(int movieId)
        {
            return _db.Table<MovieActor>().Where(ma => ma.MovieId == movieId).DeleteAsync();
        }

        public async Task<List<int>> GetActorIdsByMovieIdAsync(int movieId)
        {
            var result = await _db.Table<MovieActor>().Where(ma => ma.MovieId == movieId).ToListAsync();
            return result.Select(ma => ma.ActorId).ToList();
        }

        public async Task<List<int>> GetMovieIdsByActorIdAsync(int actorId)
        {
            var results = await _db.Table<MovieActor>().Where(ma => ma.ActorId == actorId).ToListAsync();
            return results.Select(ma => ma.MovieId).ToList();
        }
    }
}
