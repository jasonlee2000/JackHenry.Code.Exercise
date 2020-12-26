using System.Threading.Tasks;
using JackHenry.CodeExercise.App.Services.Interfaces.Counters;
using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using Microsoft.Extensions.Logging;

namespace JackHenry.CodeExercise.App.Services.Counters
{
    public class UrlDomainCounter : Counter, IUrlDomainCounter
    {
        private readonly IUrlDomainMatcher _matcher;

        public UrlDomainCounter(
            ILogger<UrlDomainCounter> logger,
            IUrlDomainMatcher matcher): base(logger)
        {
            _matcher = matcher;
        }

        public override async Task Count(string input)
        {
            var domains = await _matcher.Match(input);

            foreach (var domain in domains)
            {
                IncrementCounter(CounterService.UrlDomainCount, domain);
            }
        }
    }
}
