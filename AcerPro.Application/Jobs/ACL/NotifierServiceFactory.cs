using AcerPro.Persistence.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace AcerPro.Application.Jobs.ACL;

public class NotifierServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public NotifierServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public INotifierService GetNotifierService(NotifierTypeDto notifierType)
    {
        try
        {
            return notifierType switch
            {
                NotifierTypeDto.Email => _serviceProvider.GetRequiredService<EmailNotifierService>(),
                NotifierTypeDto.SMS => _serviceProvider.GetRequiredService<SMSNotifierService>(),
                NotifierTypeDto.Call => _serviceProvider.GetRequiredService<CallNotifierService>(),
                _ => throw new ArgumentException("Invalid notifier type"),
            };
        }
        catch (Exception ex)
        {

            throw;
        }

    }
}