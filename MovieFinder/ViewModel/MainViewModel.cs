using BLMovieFinder;
using BLMovieFinder.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieFinder.Database;
using MovieFinder.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;


namespace MovieFinder.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private ObservableCollection<MovieViewModel> _movies = new();
        public ObservableCollection<MovieViewModel> Movies
        {
            get => _movies;
            private set => SetProperty(ref _movies, value);
        }


        private readonly IMovieRepository _movieRepository;
        private CancellationTokenSource _cancellationTokenSource = new();

        private string _searchByTitle = string.Empty;
        public string SearchByTitle
        {
            get => _searchByTitle;
            set
            {
                if (SetProperty(ref _searchByTitle, value))
                {
                    DebounceUpdateMovies();
                }
            }
        }

        private string _searchByGenre = string.Empty;
        public string SearchByGenre
        {
            get => _searchByGenre;
            set
            {
                if (SetProperty(ref _searchByGenre, value))
                {
                    DebounceUpdateMovies();
                }
            }
        }

        private string _searchByActor = string.Empty;
        public string SearchByActor
        {
            get => _searchByActor;
            set
            {
                if (SetProperty(ref _searchByActor, value))
                {
                    DebounceUpdateMovies();
                }
            }
        }

        public ICommand UpdateMoviesCommand { get; }
        public ICommand SelectionChangedCommand { get; }

        private MovieViewModel selectedMovie;

        public MovieViewModel SelectedMovie
        {
            get => selectedMovie;
            set => SetProperty(ref selectedMovie, value);
        }

        public MainViewModel(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            UpdateMoviesCommand = new AsyncRelayCommand(UpdateMoviesAsync);
            SelectionChangedCommand = new AsyncRelayCommand(OpenMovieDetails);
        }


        private async Task OpenMovieDetails()
        {
            if (SelectedMovie == null) return;

            var navigationParameter = new Dictionary<string, object>
            {
                { "Movie", SelectedMovie }
            };

            await Shell.Current.GoToAsync(nameof(MovieDetailView), navigationParameter); ;
        }

        private async Task<ObservableCollection<MovieViewModel>> SearchMovie(string title, string genre, string actor)
        {
            var movies = await _movieRepository.SearchMoviesAsync(title, genre, actor);
            var movieViewModels = movies.Select(m => new MovieViewModel(new MovieDTO
            {
                Title = m.Title,
                Genre = m.Genre,
                ReleaseYear = m.ReleaseYear,
                ActorsText = m.ActorsText,
                Director = m.Director,
                Fees = m.Fees,
                Description = m.Description
            })).ToList();

            return new ObservableCollection<MovieViewModel>(movieViewModels);
        }

        private async void DebounceUpdateMovies()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            try
            {
                await Task.Delay(1000, token);
                if (!token.IsCancellationRequested)
                {
                    await UpdateMoviesAsync();
                }
            }
            catch (TaskCanceledException) { }
            
        }

        public async Task UpdateMoviesAsync()
        {
            var movies = await SearchMovie(SearchByTitle, SearchByGenre, SearchByActor);
            Movies = new ObservableCollection<MovieViewModel>(movies);
        }
    }
}
