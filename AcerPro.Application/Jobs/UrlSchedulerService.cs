using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AcerPro.Application.Jobs;

public class UrlSchedulerService : BackgroundService
{
    private readonly UrlScheduler _urlScheduler;
    private readonly ILogger<UrlSchedulerService> _logger;

    public UrlSchedulerService(UrlScheduler urlScheduler, ILogger<UrlSchedulerService> logger)
    {
        _urlScheduler = urlScheduler;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _urlScheduler.StartAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }

            await _urlScheduler.StopAsync(stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _urlScheduler.StopAsync(cancellationToken);
    }

    public async Task RestartAsync(CancellationToken cancellationToken)
    {
        await StopAsync(cancellationToken);
        await StartAsync(cancellationToken);
    }
}
