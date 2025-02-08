# Movie Finder

Movie Finder — это приложение на платформе .NET MAUI, которое позволяет пользователям искать фильмы по названию, жанру и актерам, а также просматривать подробную информацию о выбранном фильме. Приложение использует SQLite для хранения данных о фильмах и их актерах.

## Особенности
- Поиск фильмов по названию, жанру и актеру.
- Просмотр подробной информации о фильме, включая название, жанр, год выпуска, актеров, режиссера, сборы и описание.
- Использование паттерна MVVM (Model-View-ViewModel) для разделения логики и представления.
- Использование SQLite для хранения и работы с данными.
- Реализация дебаунса для оптимизации поисковых запросов.

## Технологии
- .NET MAUI (Multiplatform App UI)
- MVVM (Model-View-ViewModel)
- SQLite для хранения данных
- CommunityToolkit.Mvvm для упрощения работы с командой и свойствами

## Установка

Для того чтобы запустить проект, выполните следующие шаги:

1. Клонируйте репозиторий на свою локальную машину:

    ```bash
    git clone https://github.com/yourusername/movie-finder.git
    ```

2. Откройте проект в [Visual Studio](https://visualstudio.microsoft.com/) или другой IDE с поддержкой .NET MAUI.

3. Установите все зависимости, если они еще не установлены:

    ```bash
    dotnet restore
    ```

4. Запустите приложение:

    ```bash
    dotnet build
    dotnet run
    ```

## Структура проекта

### Папка `MovieFinder.View`
Содержит представления (views) и разметку для экрана поиска фильмов и подробной информации о фильмах.

- **MainView.xaml** — главный экран поиска фильмов.
- **MovieDetailView.xaml** — экран подробной информации о выбранном фильме.

### Папка `MovieFinder.ViewModel`
Содержит модели представлений (view models), которые управляют логикой приложения.

- **MainViewModel.cs** — модель представления для главного экрана.
- **MovieDetailViewModel.cs** — модель представления для экрана подробной информации.

### Папка `MovieFinder.Database`
Содержит классы, связанные с работой с базой данных SQLite.

- **MovieRepository.cs** — репозиторий для работы с данными о фильмах.
- **ActorRepository.cs** — репозиторий для работы с данными о актерах.
- **MovieActorRepository.cs** — репозиторий для связи фильмов и актеров.

### Папка `BLMovieFinder`
Содержит бизнес-логику приложения и модели данных.

- **Models**
  - **Movie.cs** — модель для фильма.
  - **Actor.cs** — модель для актера.

### Папка `MovieFinder`
Содержит конфигурацию и инициализацию приложения.

- **App.xaml.cs** — основной файл приложения.
- **AppShell.xaml.cs** — файл навигации.
- **MauiProgram.cs** — конфигурация DI контейнера и зависимостей.

## Навигация

- **Главный экран**: Пользователь вводит критерии поиска (название фильма, жанр, актер) и нажимает кнопку "Поиск".
- **Экран деталей фильма**: При выборе фильма отображаются подробности, такие как название, жанр, год выпуска, актеры, режиссер, сборы и описание.

## Тестирование

Для тестирования проекта используются юнит-тесты, которые помогают гарантировать правильность работы бизнес-логики и взаимодействия с базой данных.

### Установка зависимостей для тестирования

1. Убедитесь, что у вас установлен [xUnit](https://xunit.net/) — фреймворк для тестирования.
2. Установите зависимости для тестирования:

    ```bash
    dotnet add package xUnit
    dotnet add package xUnit.runner.visualstudio
    dotnet add package Microsoft.NET.Test.Sdk
    ```

### Написание тестов

- Тесты для бизнес-логики, такие как работа с репозиториями и фильтрация данных, можно найти в папке `MovieFinder.Tests`.
- Например, тестирование репозитория фильмов:

```csharp
using Xunit;
using MovieFinder.Database;
using Moq;
using System.Threading.Tasks;

public class MovieRepositoryTests
{
    private readonly Mock<IMovieRepository> _movieRepositoryMock;

    public MovieRepositoryTests()
    {
        _movieRepositoryMock = new Mock<IMovieRepository>();
    }

    [Fact]
    public async Task SearchMoviesAsync_ShouldReturnMovies()
    {
        // Arrange
        var movieList = new List<Movie> { new Movie { Title = "Inception" } };
        _movieRepositoryMock.Setup(repo => repo.SearchMoviesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(movieList);

        // Act
        var result = await _movieRepositoryMock.Object.SearchMoviesAsync("Inception", "", "");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Inception", result[0].Title);
    }
}
