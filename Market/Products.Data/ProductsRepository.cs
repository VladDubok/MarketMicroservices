using Common;
using Common.Entities;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Products.Data;
public class ProductsRepository : IProductsRepository
{
    private readonly NpgsqlConnection _connection;

    public ProductsRepository(IOptions<DatabaseOptions> databaseOptions)
    {
        _connection = new NpgsqlConnection(databaseOptions.Value.ConnectionString);
    }

    public async Task<Product> GetAsync(int id)
    {
        var parameters = new { id };
        var query = @"
            select * from Products p
            where p.Id = @id
        ";
        var product = await _connection.QueryFirstOrDefaultAsync<Product>(query, parameters);

        return product;
    }

    public async Task<Product[]> GetAllAsync()
    {
        var query = @"
            select * from Products p
        ";
        var product = await _connection.QueryAsync<Product>(query);

        return product.ToArray();
    }

    public async Task AddAsync(Product product)
    {
        var query = @"
            insert into Products(Name, Price, AmountLeft, CreatedAt)
            values
            (@Name, @Price, @AmountLeft, @CreatedAt)
        ";
        await _connection.ExecuteAsync(query, product);
    }

    public async Task UpdateAsync(Product product)
    {
        var query = @"
            update Products p
            set p.Name = @Name,
                p.Price = @Price,
                p.AmountLeft = @AmountLeft,
                p.UpdatedAt = @UpdatedAt,
        ";
        await _connection.ExecuteAsync(query, product);
    }

    public async Task DeleteAsync(int id)
    {
        var parameters = new { id };
        var query = @"
            update Products p
            set p.IsDeleted = true
            where p.Id = @id
        ";

        await _connection.ExecuteAsync(query, parameters);
    }
}