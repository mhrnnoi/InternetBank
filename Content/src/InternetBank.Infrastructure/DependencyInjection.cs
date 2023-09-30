using System.Reflection;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using InternetBank.Infrastructure.Data;
using InternetBank.Infrastructure.Repositories;
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
        services.AddScoped<IAppUserRepository, AppUserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql("Host=localhost;Port=5432;Database=InternetBankDb;Username=mehran;Password=MyPassword@complex3343;");
        });

        var typeadapterConfig = TypeAdapterConfig.GlobalSettings;
        typeadapterConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(typeadapterConfig);
        services.AddScoped<IMapper, ServiceMapper>();
        

        return services;

    }
}
