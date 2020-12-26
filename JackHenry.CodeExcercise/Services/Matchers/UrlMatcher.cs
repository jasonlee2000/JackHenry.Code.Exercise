using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using Microsoft.Extensions.Logging;

namespace JackHenry.CodeExercise.App.Services.Matchers
{
    public class UrlMatcher : Matcher, IUrlMatcher
    {
        public override string Re { get; } = @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";

        public UrlMatcher(ILogger<UrlMatcher> logger): base(logger)
        {
        }

        public override Task<List<string>> Match(string input)
        {
            var matches = Regex.Matches(input, Re);

            var urls = matches.Select(m => m.Value);

            return Task.FromResult(urls.ToList());
        }
    }
}
