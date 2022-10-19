using SaitynoGerasis.Data.Entities;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace SaitynoGerasis.Data.Repositories;

public interface ISoldProductRepository
{
    Task<perkamapreke?> GetAsync(int soldId);
    Task<IReadOnlyList<perkamapreke>> GetManyAsync();
    Task CreateAsync(perkamapreke soldItem);
    Task UpdateAsync(perkamapreke soldItem);
    Task DeleteAsync(perkamapreke soldItem);
}

public class SoldProductRepository : ISoldProductRepository
{
    private readonly ShopDbContext _shopDbContext;

    public SoldProductRepository(ShopDbContext shopDbContext)
    {
        _shopDbContext = shopDbContext;
    }

    public async Task<perkamapreke?> GetAsync(int soldId)
    {
        return await _shopDbContext.perkamapreke.FirstOrDefaultAsync(o => o.id == soldId);
    }

    public async Task<IReadOnlyList<perkamapreke>> GetManyAsync()
    {
        return await _shopDbContext.perkamapreke.ToListAsync();
    }


    public async Task CreateAsync(perkamapreke soldItem)
    {
        _shopDbContext.perkamapreke.Add(soldItem);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(perkamapreke soldItem)
    {
        _shopDbContext.perkamapreke.Update(soldItem);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(perkamapreke soldItem)
    {
        _shopDbContext.perkamapreke.Remove(soldItem);
        await _shopDbContext.SaveChangesAsync();
    }
}