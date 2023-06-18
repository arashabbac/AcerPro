using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using Quartz;

namespace AcerPro.Application.Jobs;

public class DIJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DIJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        var jobDetail = bundle.JobDetail;
        var jobType = jobDetail.JobType;

        try
        {
            return (IJob)_serviceProvider.GetRequiredService(jobType);
        }
        catch (Exception ex)
        {
            throw new SchedulerException($"Failed to instantiate job {jobType}.", ex);
        }
    }

    public void ReturnJob(IJob job)
    {
    }
}
