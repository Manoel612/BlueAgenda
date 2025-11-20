using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BlueAgenda.Infrastructure.Data
{
    public class BlueAgendaDbContextFactory : IDesignTimeDbContextFactory<BlueAgendaDbContext>
    {
        public BlueAgendaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlueAgendaDbContext>();

            var connectionString = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../BlueAgenda.Api"))
                .AddJsonFile("appsettings.json")
                .Build().GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new BlueAgendaDbContext(optionsBuilder.Options);
        }
    }
}