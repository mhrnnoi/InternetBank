using Asp.Versioning;
using InternetBank.Api.MyProblemDetails;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace InternetBank.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSingleton<ProblemDetailsFactory, MyProblemDetailsFactory>();
        services.AddApiVersioning(
            options =>
            {
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();

            }
        );

        return services;

    }
}
