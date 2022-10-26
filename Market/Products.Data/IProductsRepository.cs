using Common.Entities;

namespace Products.Data;

public interface IProductsRepository
{
    public Task<Product> GetAsync(int id);
    public Task<Product[]> GetAllAsync();
    public Task AddAsync(Product product);
    public Task UpdateAsync(Product product);
    public Task DeleteAsync(int id);
}
