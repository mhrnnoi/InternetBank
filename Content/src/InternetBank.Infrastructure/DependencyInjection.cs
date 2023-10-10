using System.Reflection;
using InternetBank.Application.Common.Interfaces;
using InternetBank.Domain.Interfaces.IdentityService;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Infrastructure.Data;
using InternetBank.Infrastructure.Identity;
using InternetBank.Infrastructure.Services;
using InternetBank.Infrastructure.UOF;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBank.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IMapper, ServiceMapper>();
        var typeadapterConfig = TypeAdapterConfig.GlobalSettings;
        typeadapterConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(typeadapterConfig);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql("Host=localhost;Port=5432;Database=InternetBankDb;Username=mehran;Password=MyPassword@complex3343;");
        });




        return services;

    }
}
