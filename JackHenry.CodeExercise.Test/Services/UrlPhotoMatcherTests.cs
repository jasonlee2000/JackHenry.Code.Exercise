using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using JackHenry.CodeExercise.App.Services.Matchers;

namespace JackHenry.CodeExercise.Test.Services
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class UrlPhotoMatcherTests
    {
        private readonly IMock<ILogger<UrlPhotoMatcher>> _loggerMock = new Mock<ILogger<UrlPhotoMatcher>>();
        private IUrlPhotoMatcher _matcher;

        [SetUp]
        public void Setup()
        {
            _matcher = new UrlPhotoMatcher(_loggerMock.Object);
        }

        [Test]
        public async Task UrlPhotoMatcher_Match_Successfully()
        {
            // Arrange
            string input = "http://pic.twitter.com/iutiut oiuyoiuyti https://pic.twitter.com/iutiut oiuyoiuyti https://pic.Instagram.com/iutiut oiuyoiuyti http://pic.twiTter.com/iutiut oiuyoiuyti https://pic.twitter.com/iutiut oiuyoiuyti https://pic.instagram.com/iutiut oiuyoiuyti";

            // Act
            var matches = await _matcher.Match(input);

            // Assert
            Assert.AreEqual(6, matches.Count);
            Assert.IsTrue(matches.Contains("https://pic.twitter.com/iutiut"));
            Assert.IsTrue(matches.Contains("https://pic.Instagram.com/iutiut"));
        }
    }
}