using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AcerPro.Domain.Contracts;
using AcerPro.Domain.ValueObjects;
using AcerPro.Domain.Aggregates;
using AcerPro.Persistence;

namespace AcerPro.Presentation.Server.Infrastructures;

public static class SeedData
{
    public static void AutoMigrationAndSeedData(this IApplicationBuilder app)
    {
        //using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
        //using var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        //context.Database.EnsureCreated();

        //var executedSeedings = context.Users.Any();
        //if (executedSeedings == false)
        //{
        //    var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        //    using var transaction = context.Database.BeginTransaction();
        //    try
        //    {
        //        for (int index = 0; index < 150; index++)
        //        {
        //            var customer = User.Create(Name.Create($"Firstname {index}").Value,
        //                Name.Create($"Lastname {index}").Value,
        //                Email.Create($"something{index}@gmail.com").Value,
        //                userRepository).Value;

        //            userRepository.AddAsync(customer);
        //        }

        //        context.SaveChanges();
        //        transaction.Commit();
        //    }
        //    catch (Exception)
        //    {
        //        transaction.Rollback();
        //    }

        //}

    }
}
