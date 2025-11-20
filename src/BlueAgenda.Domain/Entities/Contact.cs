
using System.ComponentModel.DataAnnotations;

namespace BlueAgenda.Domain.Entities;

public class Contact : BaseEntity
{
    [Required]
    [MaxLength(255)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [Phone]
    [StringLength(11, MinimumLength = 10)]
    public required string PhoneNumber { get; set; }

    [Required]
    public required DateTime BirthDate { get; set; }

    [Required]
    public required string AspNetUserId { get; set; }
}