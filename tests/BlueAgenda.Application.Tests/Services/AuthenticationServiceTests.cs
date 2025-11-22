using BlueAgenda.Application.Interfaces.Services;
using BlueAgenda.Application.Models;
using BlueAgenda.Application.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BlueAgenda.Application.Tests.Services
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IAuthenticationInfraService> MockInfraService;
        private readonly AuthenticationService Service;

        public AuthenticationServiceTests()
        {
            MockInfraService = new Mock<IAuthenticationInfraService>();
            Service = new AuthenticationService(MockInfraService.Object);
        }

        [Fact]
        public async Task RegisterAsync_ReturnsUserModel_WhenRegistrationIsSuccessful()
        {
            var model = new CreateUserModel { Email = "test@example.com", Password = "password123" };
            var user = new UserModel { Id = "user-id", Email = "test@example.com" };
            MockInfraService.Setup(s => s.RegisterAsync(model)).ReturnsAsync(user);

            var result = await Service.RegisterAsync(model);

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task LoginAsync_ReturnsToken_WhenLoginIsSuccessful()
        {
            var email = "test@example.com";
            var password = "password123";
            var token = "valid-token";
            MockInfraService.Setup(s => s.LoginAsync(email, password)).ReturnsAsync(token);

            var result = await Service.LoginAsync(email, password);

            Assert.Equal(token, result);
        }
    }
}