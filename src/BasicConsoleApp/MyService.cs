using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BasicConsoleApp
{
    public class MyService : IMyService
    {
        private readonly ILogger<MyService> _logger;
        public MyService(ILogger<MyService> logger) 
            => _logger = logger;
        public Task RunAsync()
        {
            _logger.LogInformation("Running My Service");
            return Task.CompletedTask;
        }
    }
}
