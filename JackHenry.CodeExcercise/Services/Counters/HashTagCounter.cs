using System.Threading.Tasks;
using JackHenry.CodeExercise.App.Services.Interfaces.Counters;
using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using Microsoft.Extensions.Logging;

namespace JackHenry.CodeExercise.App.Services.Counters
{
    public class HashTagCounter : Counter, IHashTagCounter
    {
        private readonly IHashTagMatcher _matcher;

        public HashTagCounter(
            ILogger<HashTagCounter> logger,
            IHashTagMatcher matcher): base(logger)
        {
            _matcher = matcher;
        }

        public override async Task Count(string input)
        {
            var matches = await _matcher.Match(input);

            foreach (var tag in matches)
            {
                IncrementCounter(CounterService.HashTagCount, tag);
            }
        }
    }
}
