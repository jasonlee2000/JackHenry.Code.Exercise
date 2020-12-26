using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using JackHenry.CodeExercise.App.Services.Matchers;

namespace JackHenry.CodeExercise.Test.Services
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class HashTagMatcherTests
    {
        private readonly IMock<ILogger<HashTagMatcher>> _loggerMock = new Mock<ILogger<HashTagMatcher>>();
        private IHashTagMatcher _matcher;

        [SetUp]
        public void Setup()
        {
            _matcher = new HashTagMatcher(_loggerMock.Object);
        }

        [Test]
        public async Task HashTagMatcher_Match_Successfully()
        {
            // Arrange
            string input = "@UNIT3J @jungkoostyles @kookfiIter @voguesevilla @yeogif @ofkooj @sug7907awcr @vobicr @sugapologetic @jeonrager @jikoliet @artdipty @knjreal poxa que amorrrr, feliz natal bebê, tudo de melhor hoje e todos os dias &lt;33";

            // Act
            var matches = await _matcher.Match(input);

            // Assert
            Assert.AreEqual(13, matches.Count);
            Assert.IsTrue(matches.Contains("@UNIT3J"));
            Assert.IsTrue(matches.Contains("@sug7907awcr"));
        }
    }
}