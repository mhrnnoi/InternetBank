using System.Reflection;
using System.Text;
using InternetBank.Application.Common.Interfaces;
using InternetBank.Application.Interfaces;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBank.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IMapper, ServiceMapper>();
        var typeadapterConfig = TypeAdapterConfig.GlobalSettings;
        typeadapterConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(typeadapterConfig);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<PublishDomainEventInterceptors>();

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddDbContext<ApplicationDbContext>((sp, optionBuilder) =>
        {

            var interceptor = sp.GetService<PublishDomainEventInterceptors>();
            optionBuilder.AddInterceptors(interceptor!);
            optionBuilder.UseNpgsql("Host=localhost;Port=5432;Database=InternetBankDb2;Username=mehran;Password=MyPassword@complex3343;");
        });

        return services;

    }
}
