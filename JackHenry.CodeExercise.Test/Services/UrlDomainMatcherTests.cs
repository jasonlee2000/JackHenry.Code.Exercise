using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using JackHenry.CodeExercise.App.Services.Matchers;
using Microsoft.Extensions.Logging;

namespace JackHenry.CodeExercise.Test.Services
{
    using System.Threading.Tasks;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class UrlDomainMatcherTests
    {
        private readonly IMock<ILogger<UrlDomainMatcher>> _loggerMock = new Mock<ILogger<UrlDomainMatcher>>();
        private IUrlDomainMatcher _matcher;

        [SetUp]
        public void Setup()
        {
            _matcher = new UrlDomainMatcher(_loggerMock.Object);
        }

        [Test]
        public async Task UrlDomainMatcher_Match_Successfully()
        {
            // Arrange
            string input = "http://pic.twitter.com/iutiut oiuyoiuyti https://pic.twitter.com/iutiut oiuyoiuyti https://pic.Instagram.com/iutiut oiuyoiuyti http://pic.twiTter.com/iutiut oiuyoiuyti https://pic.twitter.com/iutiut oiuyoiuyti https://pic.instagram.com/iutiut oiuyoiuyti";

            // Act
            var domains = await _matcher.Match(input);

            // Assert
            Assert.AreEqual(6, domains.Count);
            Assert.IsTrue(domains.Contains("pic.twitter.com"));
            Assert.IsTrue(domains.Contains("pic.Instagram.com"));
        }
    }
}