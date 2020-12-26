using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JackHenry.CodeExercise.App.Services.Interfaces.Counters;
using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using Microsoft.Extensions.Logging;

namespace JackHenry.CodeExercise.App.Services.Counters
{
    public class EmojiCounter : Counter, IEmojiCounter
    {
        private readonly IEmojiMatcher _matcher;

        public EmojiCounter(
            ILogger<EmojiCounter> logger,
            IEmojiMatcher matcher): base(logger)
        {
            _matcher = matcher;
        }

        public override async Task Count(string input)
        {
            var emojis = await _matcher.Match(input);

            this.IncrementTotal(emojis, ref CounterService.TotalTweetsContainsEmoji);

            foreach (var em in emojis)
            {
                var unicode = await GetUniCode(em);

                IncrementCounter(CounterService.EmojiCount, unicode);
            }
        }

        public Task<string> GetUniCode(string input)
        {
            var list = new List<string>();

            for (var i = 0; i < input.Length; i += char.IsSurrogatePair(input, i) ? 2 : 1)
            {
                var codepoint = char.ConvertToUtf32(input, i);

                list.Add($"{codepoint:X4}");
            }

            var unicode= string.Join("-", list);

            return Task.FromResult(unicode);
        }
    }
}
