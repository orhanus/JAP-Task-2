using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.StoredProcedureEntities;

namespace API.Interfaces
{
    public interface IReportService
    {
        Task<List<TopMostRating>> GetMoviesTopRatingAsync();
        Task<List<TopMostScreening>> GetTopScreeningsAsync(DateTime startDate, DateTime endDate);
        Task<List<TopMostSoldTicket>> GetTopMostSoldTicketsAsync();
    }
}