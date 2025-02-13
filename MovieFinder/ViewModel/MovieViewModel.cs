using CommunityToolkit.Mvvm.ComponentModel;
using MovieFinder.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFinder.ViewModel
{
    public partial class MovieViewModel : ObservableObject
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _genre;
        public string Genre
        {
            get => _genre;
            set => SetProperty(ref _genre, value);
        }

        private int _releaseYear;
        public int ReleaseYear
        {
            get => _releaseYear;
            set => SetProperty(ref _releaseYear, value);
        }

        private string _actorsText;
        public string ActorsText
        {
            get => _actorsText;
            set => SetProperty(ref _actorsText, value);
        }

        private string _director;
        public string Director
        {
            get => _director;
            set => SetProperty(ref _director, value);
        }

        private double _fees;
        public double Fees
        {
            get => _fees;
            set => SetProperty(ref _fees, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public MovieViewModel(MovieDTO dto)
        {
            Title = dto.Title;
            Genre = dto.Genre;
            ReleaseYear = dto.ReleaseYear;
            ActorsText = dto.ActorsText;
            Director = dto.Director;
            Fees = dto.Fees;
            Description = dto.Description;
        }
    }
}
