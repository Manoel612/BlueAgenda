using System.ComponentModel.DataAnnotations;

namespace BlueAgenda.Application.Models;

public class CreateUserModel
{
    [Required]
    [MaxLength(255)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    public string UserName => Email;

    [Required]
    [Phone]
    public required string PhoneNumber { get; set; }

    [Required]
    [StringLength(11, MinimumLength = 11)]
    public required string Cpf { get; set; }

    [Required]
    public required DateTime BirthDate { get; set; }

    [Required]
    public required string Password { get; set; }
}