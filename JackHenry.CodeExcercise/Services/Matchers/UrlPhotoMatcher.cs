using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using Microsoft.Extensions.Logging;

namespace JackHenry.CodeExercise.App.Services.Matchers
{
    public class UrlPhotoMatcher : Matcher, IUrlPhotoMatcher
    {
        public override string Re { get; } = @"(https?:\/\/(.+?\.)?(?i)(twitter|Instagram)\.com(\/[A-Za-z0-9\-\._~:\/\?#\[\]@!$&'\(\)\*\+,;\=]*)?)";

        public UrlPhotoMatcher(ILogger<UrlPhotoMatcher> logger): base(logger)
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
