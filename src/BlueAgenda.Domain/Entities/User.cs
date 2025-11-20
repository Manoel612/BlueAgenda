using System.ComponentModel.DataAnnotations;

namespace BlueAgenda.Domain.Entities;

public class User
{
    public required string Id { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Name { get; set; }

    [Required]
    [StringLength(11, MinimumLength = 11)]
    public required string Cpf { get; set; }

    [Required]
    public required DateTime BirthDate { get; set; }
}