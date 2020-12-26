using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using JackHenry.CodeExercise.App.Services.Matchers;

namespace JackHenry.CodeExercise.Test.Services
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class UrlMatcherTests
    {
        private readonly IMock<ILogger<UrlMatcher>> _loggerMock = new Mock<ILogger<UrlMatcher>>();
        private IUrlMatcher _matcher;

        [SetUp]
        public void Setup()
        {
            _matcher = new UrlMatcher(_loggerMock.Object);
        }

        [Test]
        public async Task HashTagMatcher_Match_Successfully()
        {
            // Arrange
            string input = "RT @rpveld: https://t.co/B8q501jwJA https://t.co/B8q501ewjwJA";

            // Act
            var matches = await _matcher.Match(input);

            // Assert
            Assert.AreEqual(2, matches.Count);
            Assert.IsTrue(matches.Contains("https://t.co/B8q501jwJA"));
            Assert.IsTrue(matches.Contains("https://t.co/B8q501ewjwJA"));
        }
    }
}