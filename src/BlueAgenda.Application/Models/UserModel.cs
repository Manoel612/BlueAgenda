namespace BlueAgenda.Application.Models;

public class UserModel
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Cpf { get; set; }
    public required DateTime BirthDate { get; set; }
    public List<ContactModel>? Contacts { get; set; }
}

public class CreateUserModel
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string UserName => Email;
    public required string PhoneNumber { get; set; }
    public required string Cpf { get; set; }
    public required DateTime BirthDate { get; set; }
    public required string Password { get; set; }
}