using AutoMapper;
using BlueAgenda.Application.Interfaces.Repositories;
using BlueAgenda.Application.Interfaces.Services;
using BlueAgenda.Application.Models;
using BlueAgenda.Application.Services;
using BlueAgenda.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BlueAgenda.Application.Tests.Services
{
    public class ContactServiceTests
    {
        private readonly Mock<IContactRepository> _mockRepository;
        private readonly Mock<IContactReadRepository> _mockReadRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ContactService _service;

        public ContactServiceTests()
        {
            _mockRepository = new Mock<IContactRepository>();
            _mockReadRepository = new Mock<IContactReadRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _service = new ContactService(
                _mockRepository.Object,
                _mockReadRepository.Object,
                _mockUnitOfWork.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsContactModel_WhenContactExists()
        {
            // Arrange
            var contactId = Guid.NewGuid();
            var contact = new Contact { Id = contactId, Name = "John Doe" };
            var contactModel = new ContactModel { Id = contactId, Name = "John Doe" };
            _mockReadRepository.Setup(r => r.GetByIdAsync(contactId)).ReturnsAsync(contact);
            _mockMapper.Setup(m => m.Map<ContactModel>(contact)).Returns(contactModel);

            // Act
            var result = await _service.GetByIdAsync(contactId);

            // Assert
            Assert.Equal(contactModel, result);
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsKeyNotFoundException_WhenContactDoesNotExist()
        {
            // Arrange
            var contactId = Guid.NewGuid();
            _mockReadRepository.Setup(r => r.GetByIdAsync(contactId)).ReturnsAsync((Contact)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(contactId));
        }

        [Fact]
        public async Task RegisterAsync_ReturnsContactModel_WhenContactIsCreated()
        {
            // Arrange
            var userId = "user-id";
            var model = new CreateContactModel { Name = "John Doe" };
            var contact = new Contact { Id = Guid.NewGuid(), Name = "John Doe", AspNetUserId = userId };
            var contactModel = new ContactModel { Id = contact.Id, Name = "John Doe" };
            _mockMapper.Setup(m => m.Map<Contact>(model)).Returns(contact);
            _mockRepository.Setup(r => r.AddAsync(contact)).ReturnsAsync(contact);
            _mockMapper.Setup(m => m.Map<ContactModel>(contact)).Returns(contactModel);

            // Act
            var result = await _service.RegisterAsync(userId, model);

            // Assert
            Assert.Equal(contactModel, result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsUpdatedContactModel_WhenContactIsUpdated()
        {
            // Arrange
            var userId = "user-id";
            var contactId = Guid.NewGuid();
            var model = new UpdateContactModel { Name = "Jane Doe" };
            var contact = new Contact { Id = contactId, Name = "John Doe", AspNetUserId = userId };
            var updatedContactModel = new ContactModel { Id = contactId, Name = "Jane Doe" };
            _mockRepository.Setup(r => r.GetById(contactId)).ReturnsAsync(contact);
            _mockMapper.Setup(m => m.Map(model, contact));
            _mockMapper.Setup(m => m.Map<ContactModel>(contact)).Returns(updatedContactModel);

            // Act
            var result = await _service.UpdateAsync(userId, contactId, model);

            // Assert
            Assert.Equal(updatedContactModel, result);
        }

        [Fact]
        public async Task DeactivateAsync_ReturnsDeactivatedContactModel_WhenContactIsDeactivated()
        {
            // Arrange
            var userId = "user-id";
            var contactId = Guid.NewGuid();
            var contact = new Contact { Id = contactId, Name = "John Doe", AspNetUserId = userId, IsActive = true };
            var deactivatedContact = new Contact { Id = contactId, Name = "John Doe", AspNetUserId = userId, IsActive = false };
            var deactivatedContactModel = new ContactModel { Id = contactId, Name = "John Doe", IsActive = false };
            _mockRepository.Setup(r => r.GetById(contactId)).ReturnsAsync(contact);
            _mockRepository.Setup(r => r.ActivateOrDeactivate(contact)).ReturnsAsync(deactivatedContact);
            _mockMapper.Setup(m => m.Map<ContactModel>(deactivatedContact)).Returns(deactivatedContactModel);

            // Act
            var result = await _service.DeactivateAsync(userId, contactId);

            // Assert
            Assert.Equal(deactivatedContactModel, result);
        }
    }
}