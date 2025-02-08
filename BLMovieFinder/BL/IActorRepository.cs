
using BLMovieFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLMovieFinder
{
    public interface IActorRepository
    {
        Task<List<Actor>> GetActorsAsync();
        Task<int> AddActorAsync(Actor actor);
        Task<int> DeleteAsync(int id);
        Task<Actor> GetByIdAsync(int id);
    }
}
