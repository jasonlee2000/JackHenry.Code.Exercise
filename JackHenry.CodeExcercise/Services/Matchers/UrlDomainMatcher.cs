﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using Microsoft.Extensions.Logging;

namespace JackHenry.CodeExercise.App.Services.Matchers
{
    public class UrlDomainMatcher : Matcher, IUrlDomainMatcher
    {
        public override string Re { get; } = @"(?:[\w-]+\.)+(?:com|net|biz)";

        public UrlDomainMatcher(ILogger<UrlDomainMatcher> logger): base(logger)
        {
        }

        public override Task<List<string>> Match(string input)
        {
            var domains = Regex.Matches(input, Re);

            var urls = domains.Select(m => m.Value);

            return Task.FromResult(urls.ToList());
        }
    }
}
