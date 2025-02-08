using MovieFinder.View;

namespace MovieFinder;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MainView), typeof(MainView));
        Routing.RegisterRoute(nameof(MovieDetailView), typeof(MovieDetailView));
    }
}
