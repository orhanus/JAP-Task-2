using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Entities.StoredProcedureEntities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
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
        

        
        public IQueryable<Show> GetShowQuery()
        {
            return _context.Shows.AsQueryable().AsNoTracking();
        }

        public async Task<PagedList<ShowDto>> GetShowsAsync(IQueryable<ShowDto> query, ShowParams showParams)
        {
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
        public async Task<ICollection<Screening>> GetScreeningsAsync()
        {
            return await _context.Screenings.OrderByDescending(s => s.ScreeningTime).ToListAsync();
        }
        public async Task<Screening> GetScreeningByIdAsync(int id)
        {
            return await _context.Screenings.Include(m => m.Movie).Include(s => s.Spectators).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddSpectatorToScreeningAsync(User user, Screening screening)
        {
            screening.Spectators.Add(user);
            user.Screenings.Add(screening);
            await _context.SaveChangesAsync();
        }
        public async Task<Screening> GetScreeningByShowIdAsync(int id)
        {
            return await _context.Screenings.FirstOrDefaultAsync(s => s.MovieId == id);
        }
        
    }
}