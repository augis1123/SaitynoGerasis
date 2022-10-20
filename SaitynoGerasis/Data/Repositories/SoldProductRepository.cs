using SaitynoGerasis.Data.Entities;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace SaitynoGerasis.Data.Repositories;

public interface ISoldProductRepository
{
    Task<perkamapreke?> GetAsync(int itemId, int billId);
    Task<perkamapreke?> GetAsync(int itemId);
    Task<IReadOnlyList<perkamapreke>> GetManyAsync(int itemId);
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

    public async Task<perkamapreke?> GetAsync(int itemId,int billId)
    {
        return await _shopDbContext.perkamapreke.FirstOrDefaultAsync(o => o.fk_SaskaitaId == billId && o.fk_PrekeId == itemId);
    }

    public async Task<perkamapreke?> GetAsync(int itemId)
    {
        return await _shopDbContext.perkamapreke.FirstOrDefaultAsync(o => o.fk_PrekeId == itemId);
    }

    public async Task<IReadOnlyList<perkamapreke>> GetManyAsync(int itemId)
    {


        return await _shopDbContext.perkamapreke.Where(o => o.fk_PrekeId == itemId).ToListAsync(); ;
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