using BLMovieFinder.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieFinder.ViewModel
{
    [QueryProperty(nameof(Movie), "Movie")]
    public partial class MovieDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private Movie movie;
    }
}

