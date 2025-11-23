namespace BlueAgenda.Application.Models;

public class ContactModel
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public DateTime? BirthDate { get; set; }
}

public class UpdateContactModel
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? BirthDate { get; set; }
}

public class CreateContactModel
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public DateTime? BirthDate { get; set; }
}