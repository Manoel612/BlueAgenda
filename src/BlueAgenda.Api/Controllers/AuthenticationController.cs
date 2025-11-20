using BlueAgenda.Application.Interfaces;
using BlueAgenda.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlueAgenda.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService AuthenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        AuthenticationService = authenticationService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] CreateUserModel model)
    {
        try
        {
            var result = await AuthenticationService.RegisterAsync(model);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(string email, string password)
    {
        try
        {
            var token = await AuthenticationService.LoginAsync(email, password);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}