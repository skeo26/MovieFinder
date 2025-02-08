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
    public class DataSeeder : IDataSeeder
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IMovieActorRepository _movieActorRepository;

        public DataSeeder(IMovieRepository movieRepository, IActorRepository actorRepository, IMovieActorRepository movieActorRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
            _movieActorRepository = movieActorRepository;
        }

        public async Task SeedAsync(SQLiteAsyncConnection db)
        {
            if (await db.Table<Movie>().CountAsync() > 0) return;

            var actors = new List<Actor>
            {
                new() { Name = "Леонардо ДиКаприо" },
                new() { Name = "Брэд Питт" },
                new() { Name = "Марго Робби" },
                new() { Name = "Мэтт Деймон" },
                new() { Name = "Джулия Робертс" },
                new() { Name = "Том Хэнкс" }
            };
            await db.InsertAllAsync(actors);

            var movies = new List<Movie>
            {
                new() { Title = "Волк с Уолл-стрит", Genre = "Драма", ReleaseYear = 2013, Director = "Мартин Скорсезе", Fees = 392000000, Description = "Фильм о взлетах и падениях брокера на Уолл-стрит." },
                new() { Title = "Однажды в Голливуде", Genre = "Комедия", ReleaseYear = 2019, Director = "Квентин Тарантино", Fees = 374000000, Description = "История двух друзей в Голливуде 1969 года." },
                new() { Title = "Интерстеллар", Genre = "Научная фантастика", ReleaseYear = 2014, Director = "Кристофер Нолан", Fees = 677000000, Description = "Группа исследователей отправляется в космос, чтобы найти новый дом для человечества." },
                new() { Title = "Джулия и Джулия", Genre = "Драма", ReleaseYear = 2009, Director = "Нора Эфрон", Fees = 129000000, Description = "История двух женщин, одна из которых изучает рецепты и жизнь Джулии Чайлд." },
                new() { Title = "Спасти рядового Райана", Genre = "Драма", ReleaseYear = 1998, Director = "Стивен Спилберг", Fees = 481000000, Description = "Группа солдат отправляется на задание по спасению одного человека во время Второй мировой войны." },
                new() { Title = "Титаник", Genre = "Драма", ReleaseYear = 1997, Director = "Джеймс Кэмерон", Fees = 2200000000, Description = "Романтическая история на фоне катастрофы легендарного корабля." }
            };
            await db.InsertAllAsync(movies);

            var insertedActors = (await db.Table<Actor>().ToListAsync()).ToDictionary(a => a.Name);
            var insertedMovies = (await db.Table<Movie>().ToListAsync()).ToDictionary(m => m.Title);

            var movieActors = new List<MovieActor>
            {
                new() { MovieId = insertedMovies["Волк с Уолл-стрит"].Id, ActorId = insertedActors["Леонардо ДиКаприо"].Id },
                new() { MovieId = insertedMovies["Однажды в Голливуде"].Id, ActorId = insertedActors["Леонардо ДиКаприо"].Id },
                new() { MovieId = insertedMovies["Однажды в Голливуде"].Id, ActorId = insertedActors["Брэд Питт"].Id },
                new() { MovieId = insertedMovies["Однажды в Голливуде"].Id, ActorId = insertedActors["Марго Робби"].Id },
                new() { MovieId = insertedMovies["Интерстеллар"].Id, ActorId = insertedActors["Мэтт Деймон"].Id },
                new() { MovieId = insertedMovies["Джулия и Джулия"].Id, ActorId = insertedActors["Джулия Робертс"].Id },
                new() { MovieId = insertedMovies["Спасти рядового Райана"].Id, ActorId = insertedActors["Том Хэнкс"].Id },
                new() { MovieId = insertedMovies["Титаник"].Id, ActorId = insertedActors["Леонардо ДиКаприо"].Id }
            };
            await db.InsertAllAsync(movieActors);
        }

    }
}


