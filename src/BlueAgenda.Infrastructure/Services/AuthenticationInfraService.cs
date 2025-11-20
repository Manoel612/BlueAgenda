using Microsoft.AspNetCore.Identity;
using BlueAgenda.Application.Models;
using BlueAgenda.Application.Interfaces;
using AutoMapper;
using BlueAgenda.Domain.Entities;
using BlueAgenda.Infrastructure.Identity;

namespace BlueAgenda.Infrastructure.Services;

public class AuthenticationInfraService : IAuthenticationInfraService
{
    private readonly IMapper Mapper;
    private readonly IJwtService JwtService;
    private readonly UserManager<AspNetUser> UserManager;
    private readonly SignInManager<AspNetUser> SignInManager;

    public AuthenticationInfraService(
        IMapper mapper,
        IJwtService jwtService,
        UserManager<AspNetUser> userManager,
        SignInManager<AspNetUser> signInManager)
    {
        Mapper = mapper;
        JwtService = jwtService;
        UserManager = userManager;
        SignInManager = signInManager;
    }

    public async Task<User> RegisterAsync(CreateUserModel model)
    {
        var user = Mapper.Map<AspNetUser>(model);
        var result = await UserManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Mapper.Map<User>(user);
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        throw new InvalidOperationException($"Falha no registro: {errors}");
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await UserManager.FindByEmailAsync(email) ?? throw new UnauthorizedAccessException("Usuário não encontrado");

        var result = await SignInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

        if (!result.Succeeded)
            throw new UnauthorizedAccessException("Usuário não autorizado");

        return JwtService.GenerateToken(Mapper.Map<User>(user));
    }

}
