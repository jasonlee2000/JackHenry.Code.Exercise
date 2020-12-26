namespace JackHenry.CodeExercise.App.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    using Models;
    using Interfaces;
    using JackHenry.CodeExercise.App.Services.Interfaces.Counters;

    public class CounterService : ICounterService
    {
        // Total Tweets
        public static int TotalTweets;

        // Top Emoji
        public static readonly ConcurrentDictionary<string, int> EmojiCount = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // Top HashTag
        public static readonly ConcurrentDictionary<string, int> HashTagCount = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // Top Url Domain
        public static readonly ConcurrentDictionary<string, int> UrlDomainCount = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // Total # of Tweets that contains Emoji
        public static int TotalTweetsContainsEmoji;

        // Total # of Tweets that contains Url
        public static int TotalTweetsContainsUrl;

        // Total # of Tweets that contains photo Url (pic.twitter.com or Instagram)
        public static int TotalTweetsContainsPhotoUrl;

        private readonly IPrinterService _printerService;
        private readonly IEmojiCounter _emojiCounter;
        private readonly IHashTagCounter _hashTagCounter;
        private readonly IUrlPhotoCounter _urlPhotoCounter;
        private readonly IUrlCounter _urlCounter;
        private readonly IUrlDomainCounter _urlDomainCounter;

        public CounterService(
            IPrinterService printerService,
            IEmojiCounter emojiCounter,
            IHashTagCounter hashTagCounter,
            IUrlPhotoCounter urlPhotoCounter,
            IUrlCounter urlCounter,
            IUrlDomainCounter urlDomainCounter)
        {
            _printerService = printerService;
            _emojiCounter = emojiCounter;
            _hashTagCounter = hashTagCounter;
            _urlPhotoCounter = urlPhotoCounter;
            _urlCounter = urlCounter;
            _urlDomainCounter = urlDomainCounter;
        }

        public async Task Count(Tweet tweet)
        {
            Interlocked.Increment(ref TotalTweets);

            await _emojiCounter.Count(tweet.Body);
            await _hashTagCounter.Count(tweet.Body);
            await _urlCounter.Count(tweet.Body);
            await _urlPhotoCounter.Count(tweet.Body);
            await _urlDomainCounter.Count(tweet.Body);

            // Print result for every 200 tweets
            if (TotalTweets % 200 == 0) {
                await _printerService.PrintTotalTweets();
                await _printerService.PrintAverageTweets(tweet);
                await _printerService.PrintTopEmoji();
                await _printerService.PrintPercentOfTweetsContainsEmoji();
                await _printerService.PrintTopHashTag();
                await _printerService.PrintPercentOfTweetsContainsUrl();
                await _printerService.PrintPercentOfTweetsContainsPhotoUrl();
                await _printerService.PrintTopUrlDomain();
            }
        }
    }
}
