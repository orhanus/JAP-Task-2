using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;

namespace API.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowRepository _showRepository;
        private readonly IAccountRepository _accountRepository;
        public ShowService(IShowRepository showRepository, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _showRepository = showRepository;
        }


        private Dictionary<string, int> GetKeywords(string searchParams)
        {
            string[] keywordsArray = { "older than ", "after " };
            Dictionary<string, int> Keywords = new Dictionary<string, int>();
            searchParams = searchParams.ToLower();

            int index = searchParams.IndexOf(keywordsArray[1]);
            if (index != -1)
            {
                Keywords.Add("after", Int32.Parse(searchParams.Substring(index + keywordsArray[0].Length, 4)));
            }

            index = searchParams.IndexOf(keywordsArray[0]);
            if (index != -1)
            {
                Keywords.Add("olderthan", Int32.Parse(searchParams.Substring(index + keywordsArray[0].Length, 1)));
            }

            bool starFilter = false;
            try
            {
                Regex regex = new Regex("at least ([1-5]) stars");
                Match match = regex.Match(searchParams);
                Keywords.Add("atleast", Int32.Parse(match.Groups[1].Value) * 2);
                starFilter = true;
            }
            catch (Exception e) { }

            if (!starFilter)
            {
                try
                {
                    Regex regex = new Regex("([1-5]) stars");
                    Match match = regex.Match(searchParams);
                    Keywords.Add("stars", Int32.Parse(match.Groups[1].Value) * 2);
                }
                catch (Exception e) { }
            }

            return Keywords;
        }

        private IQueryable<ShowDto> ApplySearchParameters(IQueryable<ShowDto> query, string searchParams)
        {
            Dictionary<string, int> keywords = GetKeywords(searchParams);
            if (keywords.Count == 0)
                query = query.Where(obj => obj.Title.ToLower().Contains(searchParams.ToLower())
                    || obj.Description.ToLower().Contains(searchParams.ToLower()));
            else
            {
                if (keywords.ContainsKey("olderthan"))
                    query = query.Where(show => DateTime.Now.Year - show.ReleaseDate.Year >= keywords["olderthan"]);
                if (keywords.ContainsKey("after"))
                    query = query.Where(show => show.ReleaseDate.Year >= keywords["after"]);
                if (keywords.ContainsKey("atleast"))
                    query = query.Where(show => show.AverageRating >= keywords["atleast"]);
                if (keywords.ContainsKey("stars"))
                    query = query.Where(show => (int)show.AverageRating == keywords["stars"]);
            }

            return query;
        }

        public async Task<PagedList<ShowDto>> GetShowsAsync(ShowParams showParams, string showType)
        {
            IQueryable<ShowDto> query = _showRepository.GetShowQuery()
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

            if (showParams.SearchParams != null)
                query = ApplySearchParameters(query, showParams.SearchParams);

            return await _showRepository.GetShowsAsync(query, showParams);
        }

        public async Task<bool> AddRating(int id, RatingDto rating)
        {
            Show show = await _showRepository.GetShowByIdAsync(id);

            if (show == null) return false;

            show.Ratings.Add(new Rating { Score = rating.Score });

            if (await _showRepository.SaveAllAsync())
            {
                return true;
            }
            return false;
        }

        public async Task ReserveTicket(int movieId, string username)
        {
            Screening screening = await _showRepository.GetScreeningByShowIdAsync(movieId);
            if (screening == null) throw new ArgumentException("Screening does not exist");
            if (screening.ScreeningTime <= DateTime.Now) throw new InvalidOperationException("Screening time is in the past");
            if (screening.Spectators.FirstOrDefault(s => s.Username == username) != null)
                throw new InvalidOperationException("You already reserved a ticket for this screening");
            User user = await _accountRepository.GetUserByUsername(username);

            await _showRepository.AddSpectatorToScreeningAsync(user, screening);
        }
    }
}