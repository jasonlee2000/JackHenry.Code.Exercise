namespace JackHenry.CodeExercise.App.Services
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;

    using Models;
    using Interfaces;

    using Microsoft.Extensions.Logging;

    public class LoaderService : ILoaderService
    {
        private readonly ILogger<LoaderService> _logger;

        private readonly Settings _config;

        private readonly ICounterService _counter;

        public LoaderService(
            ILogger<LoaderService> logger,
            Settings config,
            ICounterService counter)
        {
            _logger = logger;
            _config = config;
            _counter = counter;
        }

        public async Task Load()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                    var requestUri = _config.Url;
                    _logger.LogInformation(requestUri);

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.Token);

                    var stream = await httpClient.GetStreamAsync(requestUri);

                    using (var reader = new StreamReader(stream))
                    {
                        var startTime = DateTime.Now;

                        while (!reader.EndOfStream)
                        {
                            var currentLine = await reader.ReadLineAsync();

                            var tweet = new Tweet { StartTime = startTime, EndTime = DateTime.Now, Body = currentLine};

                            await _counter.Count(tweet);

                            Console.Write(".");
                            // _logger.LogInformation(currentLine);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
