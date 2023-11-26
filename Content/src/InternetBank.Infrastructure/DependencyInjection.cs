using System.Reflection;
using InternetBank.Application.Common.Interfaces;
using InternetBank.Application.Interfaces;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using InternetBank.Infrastructure.BackgroundJobs;
using InternetBank.Infrastructure.Data;
using InternetBank.Infrastructure.Identity;
using InternetBank.Infrastructure.Interceptors;
using InternetBank.Infrastructure.Repositories;
using InternetBank.Infrastructure.Services;
using InternetBank.Infrastructure.UOF;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace InternetBank.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
 {
     var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

     configure.AddJob<ProcessOutboxMessagesJob>(jobKey)
         .AddTrigger(
             trigger => trigger.ForJob(jobKey)
             .WithSimpleSchedule(schedule =>
             schedule.WithIntervalInSeconds(10)
             .RepeatForever())
         );

     // configure.UseMicrosoftDependencyInjectionJobFactory();

 });
        services.AddQuartzHostedService();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IMapper, ServiceMapper>();
        var typeadapterConfig = TypeAdapterConfig.GlobalSettings;
        typeadapterConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(typeadapterConfig);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ConverDomainEventToOutboxMessagesInterceptors>();

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddDbContext<ApplicationDbContext>((sp, optionBuilder) =>
        {

            var interceptor = sp.GetService<ConverDomainEventToOutboxMessagesInterceptors>();
            optionBuilder.AddInterceptors(interceptor!);
            optionBuilder.UseNpgsql("Host=localhost;Port=5432;Database=InternetBankDb2;Username=mehran;Password=MyPassword@complex3343;");
        });

        return services;

    }
}
