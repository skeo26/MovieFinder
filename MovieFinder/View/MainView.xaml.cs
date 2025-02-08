using MovieFinder.ViewModel;

namespace MovieFinder.View;

public partial class MainView : ContentPage
{
    public MainView(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}