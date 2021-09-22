using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class ShowsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IShowService _showService;
        public ShowsController(IShowService showService, IMapper mapper)
        {
            _showService = showService;
            _mapper = mapper;
        }

        [HttpGet("{showType}", Name = "GetShows")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetShows(
            [FromQuery] ShowParams showParams,
            [RegularExpression(@"^(all|movie|show)$", ErrorMessage = "Category does not exist")] string showType
            )
        {
            PagedList<ShowDto> shows = await _showService.GetShowsAsync(showParams, showType);
            Response.AddPaginationHeader(shows.CurrentPage, shows.PageSize, shows.TotalCount, shows.TotalPages);
            return Ok(shows);
        }

        [HttpPost("{id}/rate")]
        public async Task<ActionResult> AddRating(int id, RatingDto rating)
        {
            if (await _showService.AddRating(id, rating))
            {
                return Ok();
            }
            return BadRequest("Unable to add rating");
        }
        [Authorize]
        [HttpPost("{movieId}/ticket")]
        public async Task<ActionResult> ReserveTicket(int movieId)
        {
            var username = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _showService.ReserveTicket(movieId, username);
            return Ok();
        }

    }
}