using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITests
{
    [TestFixture]
    public class AddRatingTest
    {
        IShowService showService;
        Mock<IShowRepository> mockShowRepository;
        Mock<IAccountRepository> mockAccountRepository;
        DataContext _context;

        [SetUp]
        public async Task OneTimeSetupAsync()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "tempMovieapp")
                .Options;

            _context = new DataContext(options);

            var showToReturn = new Show
            {
                Id = 1,
                Title = "Show1",
                Description = "Id nulla mollit veniam ex labore sunt dolore do aliquip nostrud aliquip irure aute est. Tempor sint Lorem elit do eu voluptate anim. Minim ex aliqua ad occaecat ad veniam non quis tempor. Magna excepteur aute nisi exercitation ea commodo ut reprehenderit nostrud eu ipsum reprehenderit commodo. Elit amet nulla ut eiusmod est duis non est nostrud ex. Elit mollit veniam aute anim ad irure dolor et pariatur. Voluptate laborum laboris ullamco ullamco excepteur dolor est quis dolore dolor dolor.\r\n",
                ReleaseDate = DateTime.Now,
                CoverImageUrl = "https://picsum.photos/id/1054/200/300",
                ShowType = "show",
                Ratings = new List<Rating>() 
            };

            _context.Shows.Add(showToReturn);

            mockShowRepository = new Mock<IShowRepository>();
            mockShowRepository.Setup(x => x.GetShowByIdAsync(It.IsAny<int>())).Returns(async () => await _context.Shows.FindAsync(1));
            mockShowRepository.Setup(x => x.SaveAllAsync()).Returns(async () => await _context.SaveChangesAsync() > 0 );

            mockAccountRepository = new Mock<IAccountRepository>();
            showService = new ShowService(mockShowRepository.Object, mockAccountRepository.Object);
        }
        [TearDown]
        public async Task TearDownAsync()
        {
            await _context.Database.EnsureDeletedAsync();
        }

        [Test]
        public async Task AddRating_ValidScore_VerifyRepositoryMethodsGotCalled()
        {
            RatingDto rating = new RatingDto { Score = 9 };


            await showService.AddRating(1, rating);

            var ratingCount = _context.Ratings.Count();

            mockShowRepository.Verify(x => x.GetShowByIdAsync(1), Times.Once);
            mockShowRepository.Verify(x => x.SaveAllAsync(), Times.Once);
        }
        [Test]
        public async Task AddRating_ValidScore_AddsToDatabase()
        {
            RatingDto rating = new RatingDto { Score = 10 };


            await showService.AddRating(1, rating);

            var ratingCount = _context.Ratings.Count();

            Assert.That(ratingCount, Is.EqualTo(1));
        }
    }
}
