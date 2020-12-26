using JackHenry.CodeExercise.App.Services.Counters;
using JackHenry.CodeExercise.App.Services.Interfaces.Counters;

namespace JackHenry.CodeExercise.Test.Services
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class EmojiCounterTests
    {
        private readonly IMock<ILogger<EmojiCounter>> _loggerMock = new Mock<ILogger<EmojiCounter>>();
        private IEmojiCounter _counter;

        [SetUp]
        public void Setup()
        {
            _counter = new EmojiCounter(_loggerMock.Object, null);
        }

        [Test]
        public async Task EmojiCounter_GetUniCode_Successfully()
        {
            // Arrange
            string input = "❤️";

            // Act
            var output = await _counter.GetUniCode(input);

            // Assert
            Assert.AreEqual("2764-FE0F", output);
        }
    }
}