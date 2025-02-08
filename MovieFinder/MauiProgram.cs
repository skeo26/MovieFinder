using BLMovieFinder;
using MovieFinder.Database;
using MovieFinder.View;
using MovieFinder.ViewModel;
using SQLite;

namespace MovieFinder;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        builder.Services.AddSingleton<AppShell>();
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "films.db");

        builder.Services.AddSingleton(new SQLiteAsyncConnection(dbPath));

        builder.Services.AddSingleton<IMovieRepository, MovieRepository>();
        builder.Services.AddSingleton<IActorRepository, ActorRepository>();
        builder.Services.AddSingleton<IMovieActorRepository, MovieActorRepository>();
        builder.Services.AddSingleton<IDataSeeder, DataSeeder>();
        builder.Services.AddSingleton<DatabaseService>();

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainView>();
        builder.Services.AddTransient<MovieDetailViewModel>();
        builder.Services.AddTransient<MovieDetailView>();

        var app = builder.Build();

        var dbService = app.Services.GetRequiredService<DatabaseService>();

        var initTask = Task.Run(dbService.InitAsync);
        initTask.Wait();

        return app;
    }
}
