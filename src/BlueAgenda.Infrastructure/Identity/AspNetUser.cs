using System.ComponentModel.DataAnnotations;
using BlueAgenda.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlueAgenda.Infrastructure.Identity;

public class AspNetUser : IdentityUser
{
    [Required]
    [MaxLength(255)]
    public required string Name { get; set; }

    [Required]
    [StringLength(11, MinimumLength = 11)]
    public required string Cpf { get; set; }

    [Required]
    public required DateTime BirthDate { get; set; }

    public IEnumerable<Contact>? Contacts { get; set; }
}