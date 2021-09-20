using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IShowRepository
    {
        void Update(Show show);
        Task<PagedList<ShowDto>> GetShowsAsync(ShowParams showParams, string showType);
        Task<Actor> GetActorByNameAsync(string name);
        Task<ICollection<Actor>> GetActorsAsync();
        Task<bool> SaveAllAsync();
        Task<double> GetAverageRatingAsync(int id);
        Task<Show> GetShowByIdAsync(int showId);
        Task<ICollection<Screening>> GetScreeningsAsync();
        Task<Screening> GetScreeningByIdAsync(int id);
        Task AddSpectatorToScreeningAsync(string username, int screeningId);
    }
}