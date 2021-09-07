using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IShowRepository
    {
        void Update(Show show);
        Task<ICollection<Show>> GetShowsAsync(string showType);
        Task<Actor> GetActorByNameAsync(string name);
        Task<ICollection<Actor>> GetActorsAsync();
        Task<bool> SaveAllAsync();
    }
}