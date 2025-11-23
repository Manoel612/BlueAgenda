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

    public async Task<UserContactsModel> GetByUserIdAsync(
        string aspNetUserId,
        int page = 1,
        int pageSize = 20)
    {
        var sqlItems = @"
            SELECT 
                Id,
                Name,
                Email,
                PhoneNumber,
                BirthDate,
                CreatedAt
            FROM Contacts
            WHERE AspNetUserId = @UserId
            AND InactivatedAt IS NULL
            ORDER BY CreatedAt DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
        ";

        var sqlCount = @"
            SELECT COUNT(*) 
            FROM Contacts
            WHERE AspNetUserId = @UserId;
        ";

        var parameters = new DynamicParameters();
        parameters.Add("UserId", aspNetUserId);
        parameters.Add("Offset", (page - 1) * pageSize);
        parameters.Add("PageSize", pageSize);

        using var conn = Factory.CreateConnection();

        var items = await conn.QueryAsync<ContactModel>(sqlItems, parameters);

        var TotalItems = await conn.ExecuteScalarAsync<int>(sqlCount, new { UserId = aspNetUserId });

        return new UserContactsModel
        {
            Items = items,
            TotalItems = TotalItems
        };
    }

}
