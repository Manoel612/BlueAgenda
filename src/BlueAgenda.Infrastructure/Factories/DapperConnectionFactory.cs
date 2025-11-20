using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BlueAgenda.Infrastructure.Factories;

public class DapperConnectionFactory
{
    private readonly string ConnectionString;

    public DapperConnectionFactory(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Connection string not found.");
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(ConnectionString);
    }
}