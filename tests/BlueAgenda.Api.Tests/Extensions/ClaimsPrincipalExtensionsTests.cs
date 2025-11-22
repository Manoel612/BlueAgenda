using BlueAgenda.Api.Extensions;
using System;
using System.Security.Claims;
using Xunit;

namespace BlueAgenda.Api.Tests.Extensions
{
    public class ClaimsPrincipalExtensionsTests
    {
        [Fact]
        public void GetUserId_ReturnsUserId_WhenUserIdClaimExists()
        {
            // Arrange
            var userId = "test-user-id";
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }));

            // Act
            var result = claimsPrincipal.GetUserId();

            // Assert
            Assert.Equal(userId, result);
        }

        [Fact]
        public void GetUserId_ThrowsUnauthorizedAccessException_WhenUserIdClaimDoesNotExist()
        {
            // Arrange
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() => claimsPrincipal.GetUserId());
            Assert.Equal("User unauthorized.", exception.Message);
        }
    }
}