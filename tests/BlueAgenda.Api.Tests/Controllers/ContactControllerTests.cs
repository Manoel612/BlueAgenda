using BlueAgenda.Api.Controllers;
using BlueAgenda.Api.Extensions;
using BlueAgenda.Application.Interfaces.Services;
using BlueAgenda.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BlueAgenda.Api.Tests.Controllers
{
    public class ContactControllerTests
    {
        private readonly Mock<IContactService> MockContactService;
        private readonly ContactController Controller;

        public ContactControllerTests()
        {
            MockContactService = new Mock<IContactService>();
            Controller = new ContactController(MockContactService.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenContactExists()
        {
            var contactId = Guid.NewGuid();
            var contact = new ContactModel { Id = contactId, Name = "John Doe" };
            MockContactService.Setup(s => s.GetByIdAsync(contactId)).ReturnsAsync(contact);

            var result = await Controller.GetById(contactId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(contact, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenContactDoesNotExist()
        {
            var contactId = Guid.NewGuid();
            MockContactService.Setup(s => s.GetByIdAsync(contactId)).ThrowsAsync(new KeyNotFoundException("Contact not found"));

            var result = await Controller.GetById(contactId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Contact not found", ((dynamic)notFoundResult.Value).Message);
        }

        [Fact]
        public async Task GetUserContacts_ReturnsOkResult_WithContacts()
        {
            var userId = "user-id";
            var contacts = new List<ContactModel> { new ContactModel { Id = Guid.NewGuid(), Name = "John Doe" } };
            MockContactService.Setup(s => s.GetByUserIdAsync(userId, 1, 20)).ReturnsAsync(contacts);
            Controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) }))
                }
            };

            var result = await Controller.GetUserContacts(1, 20);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(contacts, okResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WhenContactIsCreated()
        {
            var userId = "user-id";
            var model = new CreateContactModel { Name = "John Doe" };
            var contact = new ContactModel { Id = Guid.NewGuid(), Name = "John Doe" };
            MockContactService.Setup(s => s.RegisterAsync(userId, model)).ReturnsAsync(contact);
            Controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) }))
                }
            };

            var result = await Controller.Create(model);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(contact, okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WhenContactIsUpdated()
        {
            var userId = "user-id";
            var contactId = Guid.NewGuid();
            var model = new UpdateContactModel { Name = "Jane Doe" };
            var updatedContact = new ContactModel { Id = contactId, Name = "Jane Doe" };
            MockContactService.Setup(s => s.UpdateAsync(userId, contactId, model)).ReturnsAsync(updatedContact);
            Controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) }))
                }
            };

            var result = await Controller.Update(contactId, model);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(updatedContact, okResult.Value);
        }

        [Fact]
        public async Task Deactivate_ReturnsOkResult_WhenContactIsDeactivated()
        {
            var userId = "user-id";
            var contactId = Guid.NewGuid();
            var deactivatedContact = new ContactModel { Id = contactId, Name = "John Doe", IsActive = false };
            MockContactService.Setup(s => s.DeactivateAsync(userId, contactId)).ReturnsAsync(deactivatedContact);
            Controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) }))
                }
            };

            var result = await Controller.Deactivate(contactId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(deactivatedContact, okResult.Value);
        }
    }
}