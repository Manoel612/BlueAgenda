using BlueAgenda.Api.Extensions;
using BlueAgenda.Application.Interfaces.Services;
using BlueAgenda.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlueAgenda.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService ContactService;

    public ContactController(IContactService contactService)
    {
        ContactService = contactService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var contact = await ContactService.GetByIdAsync(id);
            return Ok(contact);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetUserContacts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        string userId = User.GetUserId();
        var contacts = await ContactService.GetByUserIdAsync(userId, page, pageSize);
        return Ok(contacts);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContactModel model)
    {
        try
        {
            string userId = User.GetUserId();
            var entity = await ContactService.RegisterAsync(userId, model);
            return Ok(entity);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContactModel model)
    {
        try
        {
            var entity = await ContactService.UpdateAsync(id, model);
            return Ok(entity);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("[action]/{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        try
        {
            var entity = await ContactService.DeactivateAsync(id);
            return Ok(entity);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
