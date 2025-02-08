
using BLMovieFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLMovieFinder
{
    public interface IMovieActorRepository
    {
        Task<List<int>> GetActorIdsByMovieIdAsync(int movieId);
        Task<List<int>> GetMovieIdsByActorIdAsync(int actorId);
        Task<int> AddAsync(MovieActor movieActor);
        Task<int> DeleteByMovieIdAsync(int movieId);
        Task<int> DeleteByActorIdAsync(int actorId);
    }
}
