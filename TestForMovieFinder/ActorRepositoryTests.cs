using BLMovieFinder;
using BLMovieFinder.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForMovieFinder
{
    public class ActorRepositoryTests
    {
        private readonly Mock<IActorRepository> _mockMovieRepository;

        public ActorRepositoryTests()
        {
            _mockMovieRepository = new Mock<IActorRepository>();
        }

        [Fact]
        public async Task GetActorsAsync_ShouldReturnListOfActors()
        {
            // Arrange
            var expectedActors = new List<Actor>
            {
                new Actor { Id = 1, Name = "Actor 1" },
                new Actor { Id = 2, Name = "Actor 2" }
            };

            _mockMovieRepository.Setup(repo => repo.GetActorsAsync()).ReturnsAsync(expectedActors);

            // Act
            var result = await _mockMovieRepository.Object.GetActorsAsync();

            // Assert
            Assert.Equal(expectedActors.Count, result.Count);
            Assert.Equal("Actor 1", result[0].Name);
        }

        [Fact]
        public async Task AddActorAsync_ShouldReturnSuccess()
        {
            // Arrange
            var actor = new Actor { Name = "New Actor" };

            _mockMovieRepository.Setup(repo => repo.AddActorAsync(It.IsAny<Actor>())).ReturnsAsync(1);

            // Act
            var result = await _mockMovieRepository.Object.AddActorAsync(actor);

            // Assert
            Assert.Equal(1, result); 
        }

        [Fact]
        public async Task GetByIdActor_ShouldReturnTrue()
        {
            // Arrange
            var expectedActor = new Actor { Id = 1, Name = "Actor 1" };
            int id = 1;

            _mockMovieRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(expectedActor);

            // Act
            var result = await _mockMovieRepository.Object.GetByIdAsync(id);

            // Assert
            Assert.Equal(expectedActor, result);
        }

        [Fact]
        public async Task DeleteActor_ShouldReturnTrue()
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
        public async Task DeleteActor_ShouldReturnZero_WhenMovieNotFound()
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
