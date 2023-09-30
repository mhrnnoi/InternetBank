using System.Reflection;
using FluentValidation;
using InternetBank.Application.Behaviours;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBank.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(options => options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;

    }
}
