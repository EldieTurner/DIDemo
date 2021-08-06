using BackgroundWorker.Example;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace BackgroundWorker.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
          => Host.CreateDefaultBuilder(args)
            .UseSystemd()
            .ConfigureServices((hostContext, services) =>
            {
                var config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .Build();
                services.AddLogging(logger =>
                {
                    logger.AddConfiguration(config.GetSection("Logging"));
                    logger.AddSimpleConsole(options =>
                    {
                        options.TimestampFormat = "MM/dd/yyyy hh:mm:ss ";
                    });
                    logger.AddDebug();
                });
                services.AddHostedService<ExampleBackgroundService>();
            });
    }
}