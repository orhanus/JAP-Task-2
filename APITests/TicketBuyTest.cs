using API.Entities;
using API.Interfaces;
using API.Services;
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
    public class TicketBuyTest
    {
        IShowService showService;
        Mock<IShowRepository> mockShowRepository;
        Mock<IAccountRepository> mockAccountRepository;
        User user;

        [SetUp]
        public void Setup()
        {
            mockShowRepository = new Mock<IShowRepository>();
            mockAccountRepository = new Mock<IAccountRepository>();
            showService = new ShowService(mockShowRepository.Object, mockAccountRepository.Object);
            user = new User { Id = 1, Username = "username" };
        }
        [Test]
        public void BuyTicket_ScreeningInFuture_CallsMethodOnce()
        {
            Screening screening = new Screening { ScreeningTime = DateTime.Now.AddDays(7) };
            
            mockAccountRepository.Setup(x => x.GetUserByUsername("username")).Returns(Task.FromResult(user));
            mockShowRepository.Setup(x => x.AddSpectatorToScreeningAsync(user, screening));
            mockShowRepository.Setup(x => x.GetScreeningByShowIdAsync(1)).Returns(Task.FromResult(screening));

            showService.ReserveTicket(1, "username");

            mockShowRepository.Verify(mock => mock.AddSpectatorToScreeningAsync(user, screening), Times.Once);
        }

        [Test]
        public void BuyTicket_ScreeningInPast_ThrowsInvalidOperationException()
        {
            Screening screening = new Screening { ScreeningTime = DateTime.Now.AddDays(-7) };

            mockAccountRepository.Setup(x => x.GetUserByUsername("username")).Returns(Task.FromResult(user));
            mockShowRepository.Setup(x => x.AddSpectatorToScreeningAsync(user, screening));
            mockShowRepository.Setup(x => x.GetScreeningByShowIdAsync(1)).Returns(Task.FromResult(screening));

            Assert.ThrowsAsync<InvalidOperationException>(() => showService.ReserveTicket(1, "username"));
        }

        [Test]
        public void BuyTicket_ScreeningInFuture_UserAlreadyBoughtTicket()
        {
            Screening screening = new Screening { ScreeningTime = DateTime.Now.AddDays(-7), Spectators = new List<User> { user } };

            mockAccountRepository.Setup(x => x.GetUserByUsername("username")).Returns(Task.FromResult(user));
            mockShowRepository.Setup(x => x.AddSpectatorToScreeningAsync(user, screening));
            mockShowRepository.Setup(x => x.GetScreeningByShowIdAsync(1)).Returns(Task.FromResult(screening));

            Assert.ThrowsAsync<InvalidOperationException>(() => showService.ReserveTicket(1, "username"));
        }
    }
}
