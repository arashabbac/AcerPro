using AcerPro.Application.Jobs.ACL;
using AcerPro.Domain.Contracts;
using AcerPro.Persistence.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Text.Json;

namespace AcerPro.Application.Jobs;

public class UrlCallerJob : IJob
{
    private readonly HttpClient _httpClient;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UrlCallerJob> _logger;

    public UrlCallerJob(IServiceProvider serviceProvider, HttpClient httpClient, ILogger<UrlCallerJob> logger)
    {
        _httpClient = httpClient;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        string serilizedApp = context.JobDetail.JobDataMap.GetString("app") ?? throw new ApplicationException("Target app must not be null");

        var app = JsonSerializer.Deserialize<TargetAppDto>(serilizedApp) ?? throw new ApplicationException("Target app must not be null");

        try
        {
            var response = await _httpClient.GetAsync(app.UrlAddress);

            if (response.IsSuccessStatusCode == false)
            {
                await Notify(app);
                await SetTargetAppIsNotHealthy(app);
                _logger.LogError($"{app.UrlAddress} has StatusCode = {response.StatusCode}");
            }
            else
            {
                await SetTargetAppIsHealthy(app);
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, $"Calling the {app.UrlAddress} has been Failed");

            await Notify(app);
            await SetTargetAppIsNotHealthy(app);
        }

    }

    private async Task Notify(TargetAppDto targetApp)
    {
        var notifierServiceFactory = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<NotifierServiceFactory>();
        foreach (var item in targetApp.Notifiers)
        {
            var service = notifierServiceFactory.GetNotifierService(item.NotifierType);
            await service.NotifyAsync(item.Address);
        }
    }

    private async Task SetTargetAppIsNotHealthy(TargetAppDto targetApp)
    {
        var userRepository = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IUserRepository>();

        var targetAppUser = await userRepository.GetByIdWithTargetAppAsync(targetApp.UserId) ??
            throw new ApplicationException($"The user of {targetApp.Name} target app not found! it might be deleted. contact your system provider!");

        var result = targetAppUser.SetTargetAppIsNotHealthy(targetApp.Id);

        if (result.IsFailed)
            throw new ApplicationException($"{result.Errors} - contact your system provider!");

        await userRepository.SaveAsync();
    }

    private async Task SetTargetAppIsHealthy(TargetAppDto targetApp)
    {
        var userRepository = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IUserRepository>();

        var targetAppUser = await userRepository.GetByIdWithTargetAppAsync(targetApp.UserId) ??
            throw new ApplicationException($"The user of {targetApp.Name} target app not found! it might be deleted. contact your system provider!");

        var result = targetAppUser.SetTargetAppIsHealthy(targetApp.Id);

        if (result.IsFailed)
            throw new ApplicationException($"{result.Errors} - contact your system provider!");

        await userRepository.SaveAsync();
    }
}
