using BlueAgenda.Application.Interfaces.Repositories;
using BlueAgenda.Application.Models;
using BlueAgenda.Infrastructure.Factories;
using Dapper;

namespace BlueAgenda.Infrastructure.Repositories.ReadRepositories;

public class ContactReadRepository : IContactReadRepository
{
    private readonly DapperConnectionFactory Factory;

    public ContactReadRepository(DapperConnectionFactory factory)
    {
        Factory = factory;
    }

    public async Task<ContactModel?> GetByIdAsync(Guid id)
    {
        const string sql = @"
            SELECT 
                Id,
                Name,
                Email,
                PhoneNumber,
                BirthDate
            FROM Contacts
            WHERE Id = @Id;
        ";

        using var conn = Factory.CreateConnection();

        return await conn.QuerySingleOrDefaultAsync<ContactModel>(sql, new { Id = id });
    }

    public async Task<IEnumerable<ContactModel>> GetByUserIdAsync(
        string aspNetUserId,
        int page = 1,
        int pageSize = 20)
    {
        var sql = @"
            SELECT 
                Id,
                Name,
                Email,
                PhoneNumber,
                BirthDate,
                CreatedAt
            FROM Contacts
            WHERE AspNetUserId = @UserId
            ORDER BY CreatedAt DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
        ";

        var parameters = new DynamicParameters();

        parameters.Add("UserId", aspNetUserId);
        parameters.Add("Offset", (page - 1) * pageSize);
        parameters.Add("PageSize", pageSize);

        using var conn = Factory.CreateConnection();
        return await conn.QueryAsync<ContactModel>(sql, parameters);
    }
}
