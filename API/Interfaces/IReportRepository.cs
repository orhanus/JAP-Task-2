using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.StoredProcedureEntities;

namespace API.Interfaces
{
    public interface IReportRepository
    {
        Task<List<TopMostSoldTicket>> GetTopMostSoldTicketsAsync();
        Task<List<TopMostScreening>> GetTopScreeningsAsync(DateTime startDate, DateTime endDate);
        Task<List<TopMostRating>> GetMoviesTopRatingAsync();
    }
}