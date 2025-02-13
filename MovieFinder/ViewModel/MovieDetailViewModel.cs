using BLMovieFinder.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using MovieFinder.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieFinder.ViewModel
{
    [QueryProperty(nameof(MovieViewModel), "Movie")]
    public partial class MovieDetailViewModel : ObservableObject
    {
        private MovieViewModel _movieViewModel;
        public MovieViewModel MovieViewModel
        {
            get => _movieViewModel;
            set => SetProperty(ref _movieViewModel, value);
        }
    }

}

