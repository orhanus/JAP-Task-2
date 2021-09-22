using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.StoredProcedureEntities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ReportController : BaseApiController
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        
        [HttpGet("movies-top-rating")]
        public async Task<ActionResult<TopMostRating>> GetMoviesWithMostRatings()
        {
            return Ok(await _reportService.GetMoviesTopRatingAsync());
        }
        [HttpGet("movies-top-ticket")]
        public async Task<ActionResult<TopMostRating>> GetMoviesWithMostSoldTickets()
        {
            return Ok(await _reportService.GetTopMostSoldTicketsAsync());
        }
        [HttpGet("movies-top-screening")]
        public async Task<ActionResult<TopMostRating>> GetMoviesWithMostScreenings(DateTime startDate, DateTime endDate)
        {
            return Ok(await _reportService.GetTopScreeningsAsync(startDate, endDate));
        }
    }
}