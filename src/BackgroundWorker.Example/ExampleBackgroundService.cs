using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundWorker.Example
{
    public class ExampleBackgroundService : BackgroundService
    {
        private const int SecondsToWait = 10;
        private readonly ILogger<ExampleBackgroundService> _logger;
        public ExampleBackgroundService(ILogger<ExampleBackgroundService> logger)
            => _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background service is started");

            while (!stoppingToken.IsCancellationRequested)
            {
                await DoAThing(stoppingToken);
                _logger.LogInformation($"Waiting {SecondsToWait} seconds");
                await Task.Delay(new TimeSpan(0,0,SecondsToWait), stoppingToken);
            }
        }

        private Task DoAThing(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Doing a thing");
            //Do something meaningful.
            return Task.Run(() => 1 + 1);
        }
    }
}


