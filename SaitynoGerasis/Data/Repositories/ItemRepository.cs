using SaitynoGerasis.Data.Entities;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace SaitynoGerasis.Data.Repositories;

public interface IItemRepository
{
    Task<preke?> GetAsync(int itemId);
    Task<IReadOnlyList<preke>> GetManyAsync();
    Task CreateAsync(preke item);
    Task UpdateAsync(preke item);
    Task DeleteAsync(preke item);
}

public class ItemRepository : IItemRepository
{
    private readonly ShopDbContext _shopDbContext;

    public ItemRepository(ShopDbContext shopDbContext)
    {
        _shopDbContext = shopDbContext;
    }

    public async Task<preke?> GetAsync(int itemId)
    {
        return await _shopDbContext.Preke.FirstOrDefaultAsync(o => o.id == itemId);
    }

    public async Task<IReadOnlyList<preke>> GetManyAsync()
    {
        return await _shopDbContext.Preke.ToListAsync();
    }


    public async Task CreateAsync(preke item)
    {
        _shopDbContext.Preke.Add(item);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(preke item)
    {
        _shopDbContext.Preke.Update(item);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(preke item)
    {
        _shopDbContext.Preke.Remove(item);
        await _shopDbContext.SaveChangesAsync();
    }
}