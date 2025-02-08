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
    public class MovieActorTests
    {
        private readonly Mock<IMovieActorRepository> _mockMovieActorRepository;

        public MovieActorTests()
        {
            _mockMovieActorRepository = new Mock<IMovieActorRepository>();
        }

        [Fact]
        public async Task AddMovieActor_ShouldReturnSuccess()
        {
            // Arrange
            var movieActor = new MovieActor { MovieId = 1, ActorId = 2 };

            _mockMovieActorRepository.Setup(repo => repo.AddAsync(movieActor)).ReturnsAsync(1);

            // Act
            var result = await _mockMovieActorRepository.Object.AddAsync(movieActor);

            // Assert
            Assert.Equal(1, result); 
        }

        [Fact]
        public async Task GetActorIdsByMovieId_ShouldReturnActorIds()
        {
            // Arrange
            int movieId = 1;
            var expectedActorIds = new List<int> { 2, 3 };

            _mockMovieActorRepository.Setup(repo => repo.GetActorIdsByMovieIdAsync(movieId)).ReturnsAsync(expectedActorIds);

            // Act
            var result = await _mockMovieActorRepository.Object.GetActorIdsByMovieIdAsync(movieId);

            // Assert
            Assert.Equal(expectedActorIds.Count, result.Count); 
            Assert.Equal(expectedActorIds[0], result[0]);
        }

        [Fact]
        public async Task DeleteByActorId_ShouldReturnSuccess()
        {
            // Arrange
            int actorIdToDelete = 2;

            _mockMovieActorRepository.Setup(repo => repo.DeleteByActorIdAsync(actorIdToDelete)).ReturnsAsync(1);

            // Act
            var result = await _mockMovieActorRepository.Object.DeleteByActorIdAsync(actorIdToDelete);

            // Assert
            Assert.Equal(1, result); 
        }
    }
}
