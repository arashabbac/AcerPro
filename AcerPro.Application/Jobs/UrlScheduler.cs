using AcerPro.Persistence.QueryRepositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using System.Text.Json;

namespace AcerPro.Application.Jobs;

public class UrlScheduler
{
    private readonly ILogger<UrlScheduler> _logger;
    private IScheduler? _scheduler;
    private readonly IServiceProvider _serviceProvider;

    public UrlScheduler(ILogger<UrlScheduler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("UrlScheduler started.");

        var repository = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ITargetAppQueryRepository>();
        var targetApps = await repository.GetAllTargetAppsWithNotifiersAsync();

        _scheduler = await StdSchedulerFactory.GetDefaultScheduler(cancellationToken);

        foreach (var app in targetApps)
        {
            var interval = new TimeSpan(0, 0, app.MonitoringIntervalInSeconds);

            var jobDetail = JobBuilder.Create<UrlCallerJob>()
                .WithIdentity(app.Id.ToString())
                .UsingJobData("app", JsonSerializer.Serialize(app))
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{app.Id}_trigger")
                .StartNow()
                .WithSimpleSchedule(x => x.WithInterval(interval).RepeatForever())
                .Build();

            await _scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
        }

        _scheduler.JobFactory = new DIJobFactory(_serviceProvider);
        await _scheduler.Start(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_scheduler != null)
            await _scheduler.Shutdown(cancellationToken);

        _logger.LogInformation("UrlScheduler stopped.");
    }
}