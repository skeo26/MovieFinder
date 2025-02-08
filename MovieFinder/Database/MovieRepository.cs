
using BLMovieFinder;
using BLMovieFinder.Models;
using SQLite;

namespace MovieFinder.Database
{
    public class MovieRepository : IMovieRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public MovieRepository(SQLiteAsyncConnection db)
        {
            _db = db;
        }

        public Task<int> AddMovieAsync(Movie movie)
        {
            return _db.InsertAsync(movie);
        }

        public Task<int> DeleteAsync(int id)
        {
            return _db.DeleteAsync<Movie>(id);
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _db.Table<Movie>().FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<List<Movie>> GetMoviesAsync()
        {
            return _db.Table<Movie>().ToListAsync();
        }

        public async Task<List<Movie>> SearchMoviesAsync(string title, string genre, string actorName)
        {
            var movies = await _db.Table<Movie>().ToListAsync();

            if (!string.IsNullOrWhiteSpace(title))
            {
                string lowerTitle = title.ToLower();
                movies = movies.Where(m => m.Title.ToLower().Contains(lowerTitle)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                string lowerGenre = genre.ToLower();
                movies = movies.Where(m => m.Genre.ToLower().Contains(lowerGenre)).ToList();
            }

            foreach (var movie in movies)
            {
                var movieActors = await _db.Table<MovieActor>()
                    .Where(ma => ma.MovieId == movie.Id)
                    .ToListAsync();

                var actorIds = movieActors.Select(ma => ma.ActorId).ToList();

                var actors = await _db.Table<Actor>()
                    .Where(a => actorIds.Contains(a.Id))
                    .ToListAsync();

                movie.ActorsText = actors.Any() ? string.Join(", ", actors.Select(a => a.Name)) : "Нет данных";
            }

            if (!string.IsNullOrWhiteSpace(actorName))
            {
                string lowerActorName = actorName.ToLower();
                var matchedActors = await _db.Table<Actor>().ToListAsync();
                var actorIds = matchedActors.Where(a => a.Name.ToLower().Contains(lowerActorName)).Select(a => a.Id).ToList();

                var matchedMovieActors = await _db.Table<MovieActor>()
                    .Where(ma => actorIds.Contains(ma.ActorId))
                    .ToListAsync();

                var movieIds = matchedMovieActors.Select(ma => ma.MovieId).ToList();

                movies = movies.Where(m => movieIds.Contains(m.Id)).ToList();
            }

            return movies;
        }
    }
}
