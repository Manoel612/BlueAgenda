using System.Data;
using System.Reflection;
using BlueAgenda.Application.Interfaces;
using BlueAgenda.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using BlueAgenda.Infrastructure.Services;

namespace BlueAgenda.Infrastructure.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<BlueAgendaDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped<IDbConnection>(provider =>
        {
            var connString = configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connString);
        });

        AddServices(services);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddIdentity<AspNetUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
        })
        .AddEntityFrameworkStores<BlueAgendaDbContext>()
        .AddDefaultTokenProviders();

        var jwtSettings = configuration.GetSection("JwtSettings");
        var teste = jwtSettings["Issuer"];
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!))
            };
        });

        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IAuthenticationInfraService, AuthenticationInfraService>();
        services.AddScoped<IJwtService, JwtService>();
    }
}