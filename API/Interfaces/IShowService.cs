using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;

namespace API.Interfaces
{
    public interface IShowService
    {
        Task<PagedList<ShowDto>> GetShowsAsync(ShowParams showParams, string showType);
        Task<bool> AddRating(int id, RatingDto rating);
        Task ReserveTicket(int movieId, string username);
    }
}