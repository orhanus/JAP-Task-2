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

        [HttpGet("{showType}")]
        public async Task<ActionResult<ICollection<ShowDto>>> GetShows(string showType)
        {
            var shows = await _showRepository.GetShowsAsync(showType);
            var showsToReturn = _mapper.Map<ICollection<ShowDto>>(shows);

            return Ok(showsToReturn);
        }
    }
}