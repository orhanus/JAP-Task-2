using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IShowRepository
    {
        void Update(Show show);
        Task<PagedList<ShowDto>> GetShowsAsync(ShowParams showParams, string showType, Dictionary<string, int> keywords);
        Task<Actor> GetActorByNameAsync(string name);
        Task<ICollection<Actor>> GetActorsAsync();
        Task<bool> SaveAllAsync();
        Task<double> GetAverageRatingAsync(int id);
        Task<ShowDto> GetShowDtoByIdAsync(int showId);
        Task<Show> GetShowByIdAsync(int showId);
        Task<ICollection<Screening>> GetScreeningsAsync();
        Task<Screening> GetScreeningByIdAsync(int id);
        Task<Screening> GetScreeningByShowIdAsync(int id);
        Task AddSpectatorToScreeningAsync(User user, Screening screening);
    }
}