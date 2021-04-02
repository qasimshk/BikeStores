using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace bs.order.service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBusControl _bus;

        public Worker(ILogger<Worker> logger, IBusControl bus)
        {
            _logger = logger;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _bus.StartAsync(stoppingToken);
            _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);

            if (stoppingToken.IsCancellationRequested)
            {
                await _bus.StopAsync(stoppingToken);
                _logger.LogInformation("Worker stopped at: {time}", DateTimeOffset.Now);
            }
        }
    }
}
