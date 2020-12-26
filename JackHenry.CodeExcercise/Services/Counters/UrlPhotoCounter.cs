using System.Threading.Tasks;
using JackHenry.CodeExercise.App.Services.Interfaces.Counters;
using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using Microsoft.Extensions.Logging;

namespace JackHenry.CodeExercise.App.Services.Counters
{
    public class UrlPhotoCounter : Counter, IUrlPhotoCounter
    {
        private readonly IUrlPhotoMatcher _matcher;

        public UrlPhotoCounter(
            ILogger<UrlPhotoCounter> logger,
            IUrlPhotoMatcher matcher): base(logger)
        {
            _matcher = matcher;
        }

        public override async Task Count(string input)
        {
            var matches = await _matcher.Match(input);

            this.IncrementTotal(matches, ref CounterService.TotalTweetsContainsPhotoUrl);
        }
    }
}
