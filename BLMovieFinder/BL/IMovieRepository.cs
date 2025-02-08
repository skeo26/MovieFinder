
using BLMovieFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLMovieFinder
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMoviesAsync();
        Task<int> AddMovieAsync(Movie movie);
        Task<int> DeleteAsync(int id);
        Task<Movie> GetByIdAsync(int id);
        Task<List<Movie>> SearchMoviesAsync(string title, string genre, string actorName);
    }
}
