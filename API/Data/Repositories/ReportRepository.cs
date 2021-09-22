using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.StoredProcedureEntities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly DataContext _context;
        public ReportRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<List<TopMostRating>> GetMoviesTopRatingAsync()
        {
            return await _context.TopMostRatings.FromSqlRaw("EXEC [dbo].[spGetTopMostRatings]").ToListAsync();
        }
        public async Task<List<TopMostScreening>> GetTopScreeningsAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.TopMostScreenings
                .FromSqlRaw("EXEC [dbo].[spGetTopWithMostScreenings] {0}, {1}", startDate, endDate).ToListAsync();
        }
        public async Task<List<TopMostSoldTicket>> GetTopMostSoldTicketsAsync()
        {
            return await _context.TopMostSoldTickets.FromSqlRaw("EXEC [dbo].[spGetTopWithMostSoldTicketsWithoutRating]").ToListAsync();
        }
    }
}