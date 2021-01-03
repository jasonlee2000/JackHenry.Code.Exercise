using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JackHenry.CodeExercise.App.Services.Interfaces.Matchers;
using Microsoft.Extensions.Logging;

namespace JackHenry.CodeExercise.App.Services.Matchers
{
    public abstract class Matcher : IMatcher
    {
        public abstract string Re { get; }
        protected readonly ILogger<Matcher> Logger;

        protected Matcher(ILogger<Matcher> logger)
        {
            Logger = logger;
        }

        public abstract Task<List<string>> Match(string input);

        protected void IncrementCounter(ConcurrentDictionary<string, int> queue, string key)
        {
            if (queue.Keys.Contains(key))
            {
                var count = queue[key];
                queue.TryAdd(key, Interlocked.Increment(ref count));
            }
            else
            {
                queue.TryAdd(key, 1);
            }
        }
    }
}
