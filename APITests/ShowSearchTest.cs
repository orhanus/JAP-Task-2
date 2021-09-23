using API.Data;
using API.Data.Repositories;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITests
{
    [TestFixture]
    public class ShowSearchTest
    {
        DataContext _context;
        IShowRepository _showRepository;
        IMapper _mapper;

        [SetUp]
        public async Task OneTimeSetupAsync()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "tempMovieapp")
                .Options;

            _context = new DataContext(options);


            _context.Shows.Add(new Show
            {
                Id = 1,
                Title = "Show1",
                Description = "Id nulla mollit veniam ex labore sunt dolore do aliquip nostrud aliquip irure aute est. Tempor sint Lorem elit do eu voluptate anim. Minim ex aliqua ad occaecat ad veniam non quis tempor. Magna excepteur aute nisi exercitation ea commodo ut reprehenderit nostrud eu ipsum reprehenderit commodo. Elit amet nulla ut eiusmod est duis non est nostrud ex. Elit mollit veniam aute anim ad irure dolor et pariatur. Voluptate laborum laboris ullamco ullamco excepteur dolor est quis dolore dolor dolor.\r\n",
                ReleaseDate = DateTime.Now,
                CoverImageUrl = "https://picsum.photos/id/1054/200/300",
                ShowType = "show",
                Ratings = new List<Rating> { new Rating { Score = 9 }, new Rating { Score = 10 } }
            });

            _context.Shows.Add(new Show
            {
                Id = 2,
                Title = "Show2",
                Description = "Id nulla mollit veniam ex labore sunt dolore do aliquip nostrud aliquip irure aute est. Tempor sint Lorem elit do eu voluptate anim. Minim ex aliqua ad occaecat ad veniam non quis tempor. Magna excepteur aute nisi exercitation ea commodo ut reprehenderit nostrud eu ipsum reprehenderit commodo. Elit amet nulla ut eiusmod est duis non est nostrud ex. Elit mollit veniam aute anim ad irure dolor et pariatur. Voluptate laborum laboris ullamco ullamco excepteur dolor est quis dolore dolor dolor.\r\n",
                ReleaseDate = DateTime.Now.AddYears(-6),
                CoverImageUrl = "https://picsum.photos/id/1054/200/300",
                ShowType = "show",
                Ratings = new List<Rating> { new Rating { Score = 4 }, new Rating { Score = 10 } }
            });

            _context.Shows.Add(new Show
            {
                Id = 3,
                Title = "Show3",
                Description = "Id nulla mollit veniam ex labore sunt dolore do aliquip nostrud aliquip irure aute est. Tempor sint Lorem elit do eu voluptate anim. Minim ex aliqua ad occaecat ad veniam non quis tempor. Magna excepteur aute nisi exercitation ea commodo ut reprehenderit nostrud eu ipsum reprehenderit commodo. Elit amet nulla ut eiusmod est duis non est nostrud ex. Elit mollit veniam aute anim ad irure dolor et pariatur. Voluptate laborum laboris ullamco ullamco excepteur dolor est quis dolore dolor dolor.\r\n",
                ReleaseDate = DateTime.Now.AddYears(-3),
                CoverImageUrl = "https://picsum.photos/id/1054/200/300",
                ShowType = "show",
                Ratings = new List<Rating>() { new Rating { Score = 10 } }
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });
            _mapper = mappingConfig.CreateMapper();

            await _context.SaveChangesAsync();

            _showRepository = new ShowRepository(_context, _mapper);

        }

        [TearDown]
        public async Task TearDownAsync()
        {
            await _context.Database.EnsureDeletedAsync();
        }


        //Throws System.InvalidCastException :
        //Unable to cast object of type 'System.Linq.Expressions.NewExpression' to type 'System.Linq.Expressions.MethodCallExpression'.
        //When invoking ToListAsync() in showRepository GetShowsAsync() in PagedList CreateAsync()
        //Unable to figure out the reason why
        [Test]
        public async Task ShowSearch_Before2019_ListOfMoviesWithReleaseDateBefore2019()
        {
            ShowParams showParams = new ShowParams { PageNumber = 1, PageSize = 10, SearchParams = "after 2019" };

            var shows = await _showRepository.GetShowsAsync(showParams, "all");

            foreach(var show in shows)
            {
                Assert.That(show.ReleaseDate.Year, Is.AtMost(2018));
            }
        }

    }
}
