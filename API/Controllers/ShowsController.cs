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

namespace API.Controllers
{
    public class ShowsController : BaseApiController
    {
        private readonly IShowRepository _showRepository;
        private readonly IMapper _mapper;
        public ShowsController(IShowRepository showRepository, IMapper mapper)
        {
            _mapper = mapper;
            _showRepository = showRepository;
        }

        [HttpGet("{showType}", Name = "GetShows")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetShows(
            [FromQuery]ShowParams showParams, 
            [RegularExpression(@"^(all|movie|show)$", ErrorMessage = "Category does not exist")]string showType
            ) {
            PagedList<ShowDto> shows = await _showRepository.GetShowsAsync(showParams, showType);
            Response.AddPaginationHeader(shows.CurrentPage, shows.PageSize, shows.TotalCount, shows.TotalPages);
            return Ok(shows);
        }

        [HttpPost("{id}/rate")]
        public async Task<ActionResult> AddRating(int id, RatingDto rating)
        {
            Show show = await _showRepository.GetShowByIdAsync(id);

            if(show == null) return BadRequest("Show does not exist");

            show.Ratings.Add(new Rating { Score = rating.Score });

            if(await _showRepository.SaveAllAsync())
            {
                return Ok();
            }
            return BadRequest("Unable to add rating");
        }
        
    }
}