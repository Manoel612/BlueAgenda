using BlueAgenda.Application.Interfaces.Services;
using BlueAgenda.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlueAgenda.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        AddServices(services);

        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IContactService, ContactService>();
    }
}