using System.Linq;
using JackHenry.CodeExercise.App.Services.Counters;
using JackHenry.CodeExercise.App.Services.Interfaces.Counters;
using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using JackHenry.CodeExercise.App.Services.Matchers;

namespace JackHenry.CodeExercise.Test.Services
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class EmojiMatcherTests
    {
        private readonly IMock<ILogger<EmojiMatcher>> _loggerMock = new Mock<ILogger<EmojiMatcher>>();
        private IEmojiMatcher _matcher;
        private IEmojiCounter _counter;

        [SetUp]
        public void Setup()
        {
            _matcher = new EmojiMatcher(_loggerMock.Object);
            _counter = new EmojiCounter(null, null);
        }

        [Test]
        public async Task EmojiMatcher_Match_Successfully()
        {
            // Arrange
            string input = "RT @_Khassia_: Ma tante Grazie Maria! Tanti cari ed affettuosi Auguri a te e famiglia. Buon natale! ❤️😘🎁🎄❄️";

            // Act
            var emojis = await _matcher.Match(input);

            // Assert
            Assert.AreEqual(5, emojis.Count);
        }
    }
}