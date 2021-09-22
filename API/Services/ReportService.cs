using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.StoredProcedureEntities;
using API.Interfaces;

namespace API.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;

        }
        public async Task<List<TopMostRating>> GetMoviesTopRatingAsync()
        {
            return await _reportRepository.GetMoviesTopRatingAsync();
        }

        public async Task<List<TopMostSoldTicket>> GetTopMostSoldTicketsAsync()
        {
            return await _reportRepository.GetTopMostSoldTicketsAsync();
        }

        public async Task<List<TopMostScreening>> GetTopScreeningsAsync(DateTime startDate, DateTime endDate)
        {
            return await _reportRepository.GetTopScreeningsAsync(startDate, endDate);
        }
    }
}