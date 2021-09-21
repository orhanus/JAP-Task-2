using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinqKit;
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
        private Dictionary<string, int> GetKeywords(string searchParams)
        {
            string[] keywordsArray = {"older than ", "after "};
            Dictionary<string, int> Keywords = new Dictionary<string, int>();
            searchParams = searchParams.ToLower();

            int index = searchParams.IndexOf(keywordsArray[1]);
            if(index != -1)
            {
                Keywords.Add("after", Int32.Parse(searchParams.Substring(index + keywordsArray[0].Length, 4)));
            }

            index = searchParams.IndexOf(keywordsArray[0]);
            if(index != -1)
            {
                Keywords.Add("olderthan", Int32.Parse(searchParams.Substring(index + keywordsArray[0].Length, 1)));
            }

            bool starFilter = false;
            try {
                Regex regex = new Regex("at least ([1-5]) stars");
                Match match = regex.Match(searchParams);
                Keywords.Add("atleast", Int32.Parse(match.Groups[1].Value) * 2);
                starFilter = true;
            }
            catch(Exception e){}

            if(!starFilter)
            {
                try {
                Regex regex = new Regex("([1-5]) stars");
                Match match = regex.Match(searchParams);
                Keywords.Add("stars", Int32.Parse(match.Groups[1].Value) * 2);
            }
            catch(Exception e){}
            }

            return Keywords;
        }

        private IQueryable<ShowDto> ApplySearchParameters(IQueryable<ShowDto> query, string searchParams)
        {
            Dictionary<string, int> keywords = GetKeywords(searchParams);
            if(keywords.Count == 0)
                query = query.Where(obj => obj.Title.ToLower().Contains(searchParams.ToLower()) 
                    || obj.Description.ToLower().Contains(searchParams.ToLower()));
            else
            {
                if(keywords.ContainsKey("olderthan"))
                    query = query.Where(show => DateTime.Now.Year - show.ReleaseDate.Year >= keywords["olderthan"]);
                if(keywords.ContainsKey("after"))
                    query = query.Where(show => show.ReleaseDate.Year >= keywords["after"]);
                if(keywords.ContainsKey("atleast"))
                    query = query.Where(show => show.AverageRating >= keywords["atleast"]);
                if(keywords.ContainsKey("stars"))
                    query = query.Where(show => (int) show.AverageRating == keywords["stars"]);
            }

            return query;
        }


        public async Task<PagedList<ShowDto>> GetShowsAsync(ShowParams showParams, string showType)
        {
            IQueryable<ShowDto> query = _context.Shows.AsQueryable().AsNoTracking()
                .Select(x => new ShowDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ReleaseDate = x.ReleaseDate,
                    CoverImageUrl = x.CoverImageUrl,
                    ShowType = x.ShowType,
                    Actors = x.Actors.Select(actor => new ActorDto { NameLastname = actor.NameLastname }).ToList(),
                    Screenings = x.Screenings.Select(screening => new ScreeningDto
                    { 
                        Id = screening.Id,
                        MovieTitle = x.Title,
                        ScreeningTime = screening.ScreeningTime,
                        Spectators = screening.Spectators.Select(spectator => new SpectatorDto { Username = spectator.Username }).ToList()
                    }).ToList(),
                    AverageRating = x.Ratings.Average(r => r.Score)
                });
            if (showType != "all")
                query = query.Where(x => x.ShowType == showType);

            if(showParams.SearchParams != null)
                query = ApplySearchParameters(query, showParams.SearchParams);

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
        public async Task AddSpectatorToScreeningAsync(string username, int screeningId)
        {
            Screening screening = await GetScreeningByIdAsync(screeningId);
            if(screening == null) throw new ArgumentException("Screening does not exist");
            if(screening.ScreeningTime <= DateTime.Now) throw new InvalidOperationException("Screening time is in the past");
            if(screening.Spectators.FirstOrDefault(s => s.Username == username) != null)
                throw new InvalidOperationException("You already reserved a ticket for this screening");
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
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