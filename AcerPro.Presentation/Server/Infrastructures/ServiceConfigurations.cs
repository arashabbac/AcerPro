using AcerPro.Domain.Contracts;
using AcerPro.Persistence;
using AcerPro.Persistence.QueryRepositories;
using AcerPro.Persistence.Repositories;
using AcerPro.Presentation.Server.Commands;
using AcerPro.Presentation.Server.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserQueryRepository, UserQueryRepository>();
        services.AddTransient<ITargetAppQueryRepository,TargetAppQueryRepository>();
    }
}
