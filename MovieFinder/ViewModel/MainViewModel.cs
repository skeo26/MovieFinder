using BLMovieFinder;
using BLMovieFinder.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        private ObservableCollection<Movie> _movies = new();
        public ObservableCollection<Movie> Movies
        {
            get => _movies;
            set => SetProperty(ref _movies, value);
        }


        private readonly IMovieRepository _movieRepository;
        private CancellationTokenSource _cancellationTokenSource = new();

        private string searchByTitle;
        public string SearchByTitle
        {
            get => searchByTitle;
            set
            {
                SetProperty(ref searchByTitle, value);
                DebounceUpdateMovies();
            }
        }

        private string searchByGenre;
        public string SearchByGenre
        {
            get => searchByGenre;
            set
            {
                SetProperty(ref searchByGenre, value);
                DebounceUpdateMovies();
            }
        }

        private string searchByActor;
        public string SearchByActor
        {
            get => searchByActor;
            set
            {
                SetProperty(ref searchByActor, value);
                DebounceUpdateMovies();
            }
        }

        public ICommand UpdateMoviesCommand { get; }
        public ICommand SelectionChangedCommand { get; }

        private Movie selectedMovie;
        public Movie SelectedMovie
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

        private async Task<ObservableCollection<Movie>> SearchMovie(string title, string genre, string actor)
        {
            var movies = await _movieRepository.SearchMoviesAsync(title, genre, actor);
            return new ObservableCollection<Movie>(movies);
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
            Movies = new ObservableCollection<Movie>(movies);
        }
    }
}
