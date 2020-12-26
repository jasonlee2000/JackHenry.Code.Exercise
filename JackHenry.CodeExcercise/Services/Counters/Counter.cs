using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JackHenry.CodeExercise.App.Services.Interfaces.Counters;
using Microsoft.Extensions.Logging;

namespace JackHenry.CodeExercise.App.Services.Counters
{
    public abstract class Counter : ICounter
    {
        protected readonly ILogger<Counter> Logger;

        protected Counter(ILogger<Counter> logger)
        {
            Logger = logger;
        }

        protected void IncrementTotal(IEnumerable<string> matches, ref int total)
        {
            if (matches.Any())
            {
                Interlocked.Increment(ref total);
            }
        }

        protected void IncrementCounter(ConcurrentDictionary<string, int> queue, string key)
        {
            if (queue.Keys.Contains(key))
            {
                var count = queue[key];
                queue[key] =Interlocked.Increment(ref count);
            }
            else
            {
                queue.TryAdd(key, 1);
            }
        }

        public virtual Task Count(string input)
        {
            throw new System.NotImplementedException();
        }
    }
}
