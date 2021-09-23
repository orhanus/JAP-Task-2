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
    public class AlternativeSearchShowsTest
    {
        IShowService showService;
        Mock<IShowRepository> mockShowRepository;
        Mock<IAccountRepository> mockAccountRepository;

        [SetUp]
        public void Setup()
        {
            mockAccountRepository = new Mock<IAccountRepository>();

            mockShowRepository = new Mock<IShowRepository>();

            showService = new ShowService(mockShowRepository.Object, mockAccountRepository.Object);
        }

        [Test]
        public void GetKeywords_After2019_ReturnDictionaryWithKeyAfterValue2019()
        {
            string input = "after 2019";

            var keywords = showService.GetKeywordsFromSearchParams(input);

            Assert.IsTrue(keywords.Keys.Contains("after"));

        }

        [Test]
        public void GetKeywords_After2019AtLeast3Stars_ReturnDictionaryWithKeywords()
        {
            string input = "after 2019 at least 3 stars";

            var keywords = showService.GetKeywordsFromSearchParams(input);

            Assert.IsTrue(keywords.Keys.Contains("after"));
            Assert.IsTrue(keywords.Keys.Contains("atleast"));
        }

        [Test]
        public void GetKeywords_OlderThan3Years_ReturnDictionaryWithKeywords()
        {
            string input = "older than 3 years";

            var keywords = showService.GetKeywordsFromSearchParams(input);

            Assert.IsTrue(keywords.Keys.Contains("olderthan"));
        }

        [Test]
        public void GetKeywords_ContradictoryStarFilter_ReturnAtLeast3Stars()
        {
            string input = "at least 3 stars with 2 stars";
            //stars are translated from 1-5 range to 1-10 range
            int expected = 3 * 2; 

            var keywords = showService.GetKeywordsFromSearchParams(input);

            Assert.IsTrue(keywords.Keys.Contains("atleast"));
            Assert.That(keywords["atleast"], Is.EqualTo(expected));
        }

        [Test]
        public void GetKeywords_After2015OlderThan2Years_KeywordsAfter2015OlderThan2()
        {
            string input = "after 2015 older than 2 years";

            var keywords = showService.GetKeywordsFromSearchParams(input);

            Assert.IsTrue(keywords.Keys.Contains("after"));
            Assert.IsTrue(keywords.Keys.Contains("olderthan"));
            Assert.That(keywords["after"], Is.EqualTo(2015));
            Assert.That(keywords["olderthan"], Is.EqualTo(2));
        }
    }
}
