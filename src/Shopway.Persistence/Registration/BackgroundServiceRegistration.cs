﻿using Quartz;
using Shopway.Persistence.BackgroundJobs;
using Shopway.Persistence.Options;

namespace Microsoft.Extensions.DependencyInjection;

internal static class BackgroundServiceRegistration
{
    internal static IServiceCollection RegisterBackgroundServices(this IServiceCollection services)
    {
        services.AddScoped<IJob, ProcessOutboxMessagesJob>();
        services.AddScoped<IJob, DeleteOutdatedSoftDeletableEntitiesJob>();
        services.AddQuartz(options =>
        {
            var shedulerId = Ulid.NewUlid();
            options.SchedulerId = $"id-{shedulerId}";
            options.SchedulerName = $"name-{shedulerId}";
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        services.ConfigureOptions<QuartzOptionsSetup>();

        return services;
    }
}
