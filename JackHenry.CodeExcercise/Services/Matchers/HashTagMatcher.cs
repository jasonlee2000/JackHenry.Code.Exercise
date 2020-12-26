using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using Microsoft.Extensions.Logging;

namespace JackHenry.CodeExercise.App.Services.Matchers
{
    public class HashTagMatcher : Matcher, IHashTagMatcher
    {
        public override string Re { get; } = "@[a-zA-Z0-9]+";

        public HashTagMatcher(ILogger<HashTagMatcher> logger): base(logger)
        {
        }

        public override Task<List<string>> Match(string input)
        {
            var matches = Regex.Matches(input, Re);

            var tags = matches.Select(m => m.Value);

            return Task.FromResult(tags.ToList());
        }
    }
}
