using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ShowRepository : IShowRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ShowRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Actor> GetActorByNameAsync(string name)
        {
            return await _context.Actors.SingleOrDefaultAsync(x => x.NameLastname == name);
        }

        public async Task<ICollection<Actor>> GetActorsAsync()
        {
            return await _context.Actors.ToListAsync();
        }


        public async Task<PagedList<ShowDto>> GetShowsAsync(ShowParams showParams, string showType)
        {
            var query = _context.Shows.AsQueryable().AsNoTracking()
                .Select(x => new ShowDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ReleaseDate = x.ReleaseDate,
                    CoverImageUrl = x.CoverImageUrl,
                    ShowType = x.ShowType,
                    Actors = x.Actors.Select(actor => new ActorDto { NameLastname = actor.NameLastname }).ToList(),
                    AverageRating = x.Ratings.Average(r => r.Score)
                });
            if (showType != "all")
                query = query.Where(x => x.ShowType == showType);

            return await PagedList<ShowDto>.CreateAsync(
                query.OrderByDescending(r => r.AverageRating), showParams.PageNumber, showParams.PageSize
            );
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Show show)
        {
            _context.Entry(show).State = EntityState.Modified;
        }

        public async Task<double> GetAverageRatingAsync(int id)
        {
            return await _context.Ratings.Where(x => x.ShowId == id).AverageAsync(r => r.Score);
        }
        public async Task<Show> GetShowByIdAsync(int showId)
        {
            return await _context.Shows.Include(r => r.Ratings).Include(a => a.Actors).Where(x => x.Id == showId).FirstOrDefaultAsync();
        }
    }
}