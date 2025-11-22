using BlueAgenda.Application.Interfaces.Services;
using BlueAgenda.Application.Services;
using BlueAgenda.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlueAgenda.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        AddServices(services);
        AddValidators(services);

        services.AddFluentValidationAutoValidation();

        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IContactService, ContactService>();
    }

    private static void AddValidators(IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateUserModelValidator>();
    }
}