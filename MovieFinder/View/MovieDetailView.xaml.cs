
using MovieFinder.ViewModel;

namespace MovieFinder.View;

public partial class MovieDetailView : ContentPage
{
    public MovieDetailView(MovieDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

