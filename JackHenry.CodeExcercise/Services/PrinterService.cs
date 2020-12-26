using System.Collections.Concurrent;

namespace JackHenry.CodeExercise.App.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Interfaces;
    using Microsoft.Extensions.Logging;

    public class PrinterService : IPrinterService
    {
        private readonly ILogger<LoaderService> _logger;

        public PrinterService(ILogger<LoaderService> logger)
        {
            _logger = logger;
        }

        public Task PrintTotalTweets()
        {
            _logger.LogInformation(string.Empty);
            _logger.LogInformation(
                $"Total number of Tweets that contains Emoji: {CounterService.TotalTweetsContainsEmoji}");
            _logger.LogInformation($"Total number of Tweets: {CounterService.TotalTweets}");

            return Task.CompletedTask;
        }

        public Task PrintAverageTweets(Tweet tweet)
        {
            double elapsedMilliseconds = (tweet.EndTime - tweet.StartTime).TotalMilliseconds;

            var perSeconds = CounterService.TotalTweets * 1000 / elapsedMilliseconds;

            _logger.LogInformation($"Average Tweets per second: {Math.Round(perSeconds)}");
            _logger.LogInformation($"Average Tweets per minute: {Math.Round(perSeconds * 60)}");
            _logger.LogInformation($"Average Tweets per hour: {Math.Round(perSeconds * 3600)}");

            return Task.CompletedTask;
        }

        public Task PrintTopEmoji()
        {
            // key of top Emoji code point
            var codePoint = GetTopCounted(CounterService.EmojiCount);

            _logger.LogInformation($"Top Emoji code point: {codePoint}");

            return Task.CompletedTask;
        }

        public Task PrintPercentOfTweetsContainsEmoji()
        {
            var percent = CounterService.TotalTweetsContainsEmoji * 1.0 / CounterService.TotalTweets;
            _logger.LogInformation($"Percent of tweets that contains emojis: {Math.Round(percent * 100)}%");

            return Task.CompletedTask;
        }

        public Task PrintTopHashTag()
        {
            if (CounterService.HashTagCount.IsEmpty)
            {
                return Task.CompletedTask;
            }

            // key of top hashtag
            var hashTag = GetTopCounted(CounterService.HashTagCount);

            _logger.LogInformation($"Top hashtag: {hashTag}");

            return Task.CompletedTask;
        }

        public Task PrintPercentOfTweetsContainsUrl()
        {
            var percent = CounterService.TotalTweetsContainsUrl * 1.0 / CounterService.TotalTweets;
            _logger.LogInformation($"Percent of tweets that contains url: {Math.Round(percent * 100)}%");

            return Task.CompletedTask;
        }

        public Task PrintPercentOfTweetsContainsPhotoUrl()
        {
            var percent = CounterService.TotalTweetsContainsPhotoUrl * 1.0 / CounterService.TotalTweets;
            _logger.LogInformation($"Percent of tweets that contains photo url: {Math.Round(percent * 100)}%");

            return Task.CompletedTask;
        }

        public Task PrintTopUrlDomain()
        {
            if (CounterService.UrlDomainCount.IsEmpty)
            {
                return Task.CompletedTask;
            }

            // key of top url domain
            var domain = GetTopCounted(CounterService.UrlDomainCount);

            _logger.LogInformation($"Top url domain: {domain}");

            return Task.CompletedTask;
        }

        static string GetTopCounted(ConcurrentDictionary<string, int> queue)
        {
            return queue.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        }
    }
}