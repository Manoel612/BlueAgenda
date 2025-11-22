using BlueAgenda.Api.Controllers;
using BlueAgenda.Application.Interfaces.Services;
using BlueAgenda.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BlueAgenda.Api.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IAuthenticationService> MockAuthService;
        private readonly AuthenticationController Controller;

        public AuthenticationControllerTests()
        {
            MockAuthService = new Mock<IAuthenticationService>();
            Controller = new AuthenticationController(MockAuthService.Object);
        }

        [Fact]
        public async Task Register_ReturnsOkResult_WhenRegistrationIsSuccessful()
        {
            var model = new CreateUserModel { Email = "test@example.com", Password = "password123" };
            MockAuthService.Setup(s => s.RegisterAsync(model)).ReturnsAsync("User registered successfully");

            var result = await Controller.Register(model);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User registered successfully", okResult.Value);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenInvalidOperationExceptionIsThrown()
        {
            var model = new CreateUserModel { Email = "test@example.com", Password = "password123" };
            MockAuthService.Setup(s => s.RegisterAsync(model)).ThrowsAsync(new InvalidOperationException("Invalid registration"));

            var result = await Controller.Register(model);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid registration", ((dynamic)badRequestResult.Value).Message);
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WhenLoginIsSuccessful()
        {
            var email = "test@example.com";
            var password = "password123";
            MockAuthService.Setup(s => s.LoginAsync(email, password)).ReturnsAsync("valid-token");

            var result = await Controller.Login(email, password);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("valid-token", ((dynamic)okResult.Value).Token);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenUnauthorizedAccessExceptionIsThrown()
        {
            var email = "test@example.com";
            var password = "password123";
            MockAuthService.Setup(s => s.LoginAsync(email, password)).ThrowsAsync(new UnauthorizedAccessException("Invalid credentials"));

            var result = await Controller.Login(email, password);

            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid credentials", ((dynamic)unauthorizedResult.Value).Message);
        }
    }
}