namespace JackHenry.CodeExercise.App
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Models;
    using Services;
    using Services.Interfaces;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Serilog;

    using Services.Counters;
    using Services.Interfaces.Counters;
    using Services.Interfaces.Matchers;
    using Services.Matchers;

    class Program
    {
        public static IConfigurationRoot configuration;

        static int Main(string[] args)
        {
            // Initialize serilog logger
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
                .WriteTo.File("Tweets.log", rollingInterval: RollingInterval.Day)
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {
                // Start!
                MainAsync(args).Wait();
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        static async Task MainAsync(string[] args)
        {
            // Create service collection
            Log.Information("Creating service collection");
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Create service provider
            Log.Information("Building service provider");
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                Log.Information("Starting service");
                await serviceProvider.GetService<ILoaderService>().Load();
                Log.Information("Ending service");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Error running service");
                throw ex;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Add logging
            serviceCollection.AddSingleton(LoggerFactory.Create(builder =>
                {
                    builder.AddSerilog(dispose: true);
                }));

            serviceCollection.AddLogging();

            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            var config = new Settings();

            configuration.GetSection("Settings").Bind(config);

            // Add access to settings
            serviceCollection.AddSingleton(config);

            // Add Loaders
            serviceCollection.AddTransient<ILoaderService, LoaderService>();
            serviceCollection.AddTransient<IPrinterService, PrinterService>();
            serviceCollection.AddTransient<ICounterService, CounterService>();

            // Add Counters
            serviceCollection.AddTransient<IEmojiCounter, EmojiCounter>();
            serviceCollection.AddTransient<IHashTagCounter, HashTagCounter>();
            serviceCollection.AddTransient<IUrlCounter, UrlCounter>();
            serviceCollection.AddTransient<IUrlPhotoCounter, UrlPhotoCounter>();
            serviceCollection.AddTransient<IUrlDomainCounter, UrlDomainCounter>();

            // Add Matchers
            serviceCollection.AddTransient<IEmojiMatcher, EmojiMatcher>();
            serviceCollection.AddTransient<IHashTagMatcher, HashTagMatcher>();
            serviceCollection.AddTransient<IUrlMatcher, UrlMatcher>();
            serviceCollection.AddTransient<IUrlPhotoMatcher, UrlPhotoMatcher>();
            serviceCollection.AddTransient<IUrlDomainMatcher, UrlDomainMatcher>();
        }
    }
}
