using System.Reflection;
using System.Text;
using InternetBank.Application.Common.Interfaces;
using InternetBank.Application.Interfaces;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using InternetBank.Infrastructure.Data;
using InternetBank.Infrastructure.Identity;
using InternetBank.Infrastructure.Repositories;
using InternetBank.Infrastructure.Services;
using InternetBank.Infrastructure.UOF;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace InternetBank.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        var cred = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!)),
                                          SecurityAlgorithms.HmacSha512);

        services.AddScoped<IMapper, ServiceMapper>();
        var typeadapterConfig = TypeAdapterConfig.GlobalSettings;
        typeadapterConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(typeadapterConfig);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql("Host=localhost;Port=5432;Database=InternetBankDb;Username=mehran;Password=MyPassword@complex3343;");
        });

        return services;

    }
}
