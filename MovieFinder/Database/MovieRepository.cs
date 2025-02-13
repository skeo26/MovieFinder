
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
            var moviesQuery = _db.Table<Movie>();

            var movies = await moviesQuery.ToListAsync(); 

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

            if (!movies.Any())
                return movies;

            var movieIds = movies.Select(m => m.Id).ToList();

            var movieActors = await _db.Table<MovieActor>()
                .Where(ma => movieIds.Contains(ma.MovieId))
                .ToListAsync();

            var actorIds = movieActors.Select(ma => ma.ActorId).Distinct().ToList();
            var actors = await _db.Table<Actor>()
                .Where(a => actorIds.Contains(a.Id))
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(actorName))
            {
                string lowerActorName = actorName.ToLower();
                var matchedActorIds = actors
                    .Where(a => a.Name.ToLower().Contains(lowerActorName))
                    .Select(a => a.Id)
                    .ToList();

                var matchedMovieIds = movieActors
                    .Where(ma => matchedActorIds.Contains(ma.ActorId))
                    .Select(ma => ma.MovieId)
                    .Distinct()
                    .ToList();

                movies = movies.Where(m => matchedMovieIds.Contains(m.Id)).ToList();
            }

            foreach (var movie in movies)
            {
                var relatedActorIds = movieActors
                    .Where(ma => ma.MovieId == movie.Id)
                    .Select(ma => ma.ActorId)
                    .ToList();

                var relatedActors = actors
                    .Where(a => relatedActorIds.Contains(a.Id))
                    .Select(a => a.Name)
                    .ToList();

                movie.ActorsText = relatedActors.Any() ? string.Join(", ", relatedActors) : "Нет данных";
            }

            return movies;
        }

    }
}
