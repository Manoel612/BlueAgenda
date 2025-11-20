using BlueAgenda.Domain.Entities;
using BlueAgenda.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlueAgenda.Infrastructure.Data;

public class BlueAgendaDbContext : IdentityDbContext<AspNetUser>
{
    public DbSet<Contact> Contacts { get; set; }

    public BlueAgendaDbContext(DbContextOptions<BlueAgendaDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly,
            x => x.Namespace == "BlueAgenda.Infrastructure.Configurations");
    }
}
