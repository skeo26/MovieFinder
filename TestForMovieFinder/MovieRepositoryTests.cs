using BLMovieFinder;
using BLMovieFinder.Models;
using Moq;

namespace TestForMovieFinder
{
    public class MovieRepositoryTests
    {
        private readonly Mock<IMovieRepository> _mockMovieRepository;

        public MovieRepositoryTests()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
        }

        [Fact]
        public async Task GetMoviesAsync_ShouldReturnListOfMovies()
        {
            // Arrange
            var expectedMovies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Movie 1" },
                new Movie { Id = 2, Title = "Movie 2" }
            };

            _mockMovieRepository.Setup(repo => repo.GetMoviesAsync()).ReturnsAsync(expectedMovies);

            // Act
            var result = await _mockMovieRepository.Object.GetMoviesAsync();

            // Assert
            Assert.Equal(expectedMovies.Count, result.Count);
            Assert.Equal("Movie 1", result[0].Title);
        }

        [Fact]
        public async Task AddMovieAsync_ShouldReturnSuccess()
        {
            // Arrange
            var movie = new Movie { Title = "New Movie" };

            _mockMovieRepository.Setup(repo => repo.AddMovieAsync(It.IsAny<Movie>())).ReturnsAsync(1);

            // Act
            var result = await _mockMovieRepository.Object.AddMovieAsync(movie);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task SearchMoviesAsync_ShouldReturnMovies()
        {
            // Arrange
            var searchQuery = "Action";
            var expectedMovies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Action Movie 1", Genre = "Action" },
                new Movie { Id = 2, Title = "Action Movie 2", Genre = "Action" }
            };

            _mockMovieRepository.Setup(repo => repo.SearchMoviesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                .ReturnsAsync(expectedMovies);

            // Act
            var result = await _mockMovieRepository.Object.SearchMoviesAsync(searchQuery, "", "");

            // Assert
            Assert.Equal(expectedMovies.Count, result.Count);
        }

        [Fact]
        public async Task GetByIdMovie_ShouldReturnTrue()
        {
            // Arrange
            var expectedMovie = new Movie { Id = 1, Title = "Movie 1" };
            int id = 1;

            _mockMovieRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(expectedMovie);

            // Act
            var result = await _mockMovieRepository.Object.GetByIdAsync(id);

            // Assert
            Assert.Equal(expectedMovie, result);
        }

        [Fact]
        public async Task DeleteMovie_ShouldReturnTrue()
        {
            // Arrange
            int idToDelete = 1;

            _mockMovieRepository.Setup(repo => repo.DeleteAsync(idToDelete)).ReturnsAsync(1);

            // Act
            var result = await _mockMovieRepository.Object.DeleteAsync(idToDelete);

            // Assert
            Assert.Equal(1, result);  
        }

        [Fact]
        public async Task DeleteMovie_ShouldReturnZero_WhenMovieNotFound()
        {
            // Arrange
            int idToDelete = 1212; 

            _mockMovieRepository.Setup(repo => repo.DeleteAsync(idToDelete)).ReturnsAsync(0);

            // Act
            var result = await _mockMovieRepository.Object.DeleteAsync(idToDelete);

            // Assert
            Assert.Equal(0, result); 
        }
    }
}