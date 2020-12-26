using System.Threading.Tasks;
using JackHenry.CodeExercise.App.Services.Interfaces.Counters;
using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using Microsoft.Extensions.Logging;

namespace JackHenry.CodeExercise.App.Services.Counters
{
    public class UrlCounter : Counter, IUrlCounter
    {
        private readonly IUrlMatcher _matcher;

        public UrlCounter(
            ILogger<UrlCounter> logger,
            IUrlMatcher matcher): base(logger)
        {
            _matcher = matcher;
        }

        public override async Task Count(string input)
        {
            var matches = await _matcher.Match(input);

            this.IncrementTotal(matches, ref CounterService.TotalTweetsContainsUrl);
        }
    }
}
