using AcerPro.Application.Commands;
using AcerPro.Application.Jobs;
using AcerPro.Application.Jobs.ACL;
using AcerPro.Application.Validators;
using AcerPro.Domain.Contracts;
using AcerPro.Persistence;
using AcerPro.Persistence.QueryRepositories;
using AcerPro.Persistence.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AcerPro.Presentation.Server.Infrastructures;

public static class ServiceConfigurations
{
    public static void AddRequiredServices(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ValidateModelStateAttribute));
        });
        services.AddFluentValidationAutoValidation(o => o.DisableDataAnnotationsValidation = true);
        services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();

        services.AddMediatR(typeof(RegisterUserCommand).Assembly);
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        services.AddHttpClient();
    }

    public static void AddDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");
            options.UseSqlServer(connectionString);
        }, ServiceLifetime.Scoped);

        services.AddDbContext<QueryDatabaseContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");
            options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
    
    public static void AddJobServices(this IServiceCollection services)
    {
        services.AddTransient<UrlScheduler>();
        services.AddTransient<UrlCallerJob>();
        services.AddTransient<NotifierServiceFactory>();
        services.AddTransient<EmailNotifierService>();
        services.AddTransient<SMSNotifierService>();
        services.AddTransient<CallNotifierService>();
        services.AddSingleton<UrlSchedulerService>();
        services.AddHostedService(provider => provider.GetRequiredService<UrlSchedulerService>());
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserQueryRepository, UserQueryRepository>();
        services.AddTransient<ITargetAppQueryRepository,TargetAppQueryRepository>();
    }
}
