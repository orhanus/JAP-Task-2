using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<ICollection<ShowDto>>> GetShows(string showType)
        {
            var shows = await _showRepository.GetShowsAsync(showType);
            var showsToReturn = _mapper.Map<ICollection<ShowDto>>(shows);

            return Ok(showsToReturn);
        }

        [HttpPost("add-rating")]
        public async Task<ActionResult<ICollection<ShowDto>>> AddRating(RatingDto rating)
        {
            var show = await _showRepository.GetShowByIdAsync(rating.ShowId);

            if(show == null) return BadRequest("Show does not exist");

            show.Ratings.Add(new Rating { Score = rating.Score });

            if(await _showRepository.SaveAllAsync())
            {
                return CreatedAtRoute("GetShows", new {showType = show.ShowType}, await _showRepository.GetShowsAsync(show.ShowType));
            }
            return BadRequest("Unable to add rating");
        }
        
    }
}