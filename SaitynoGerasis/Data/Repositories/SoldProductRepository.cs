using SaitynoGerasis.Data.Entities;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace SaitynoGerasis.Data.Repositories;

public interface ISoldProductRepository
{
    Task<perkamapreke?> GetAsync(int itemId, int billId);
    Task<perkamapreke?> GetAsync(int itemId);
    Task<perkamapreke?> GetAsyncByBill(int billId);
    Task<perkamapreke?> GetAsyncByItem(int itemId);
    Task<IReadOnlyList<perkamapreke>> GetManyAsync(int itemId);
    Task CreateAsync(perkamapreke soldItem);
    Task UpdateAsync(perkamapreke soldItem);
    Task DeleteAsync(perkamapreke soldItem);
    Task DeleteManyAsync(IReadOnlyList<perkamapreke> soldItem);
    Task DeleteManyAsyncByItems(IReadOnlyList<preke> Items);
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
    public async Task<perkamapreke?> GetAsyncByBill(int billId)
    {
        return await _shopDbContext.perkamapreke.FirstOrDefaultAsync(o => o.fk_SaskaitaId == billId);
    }

    public async Task<perkamapreke?> GetAsyncByItem(int itemId)
    {
        return await _shopDbContext.perkamapreke.FirstOrDefaultAsync(o => o.fk_SaskaitaId == itemId);
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
    public async Task DeleteManyAsync(IReadOnlyList<perkamapreke> soldItem)
    {
        foreach (perkamapreke item in soldItem)
        {
            _shopDbContext.perkamapreke.Remove(item);
        }
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task DeleteManyAsyncByItems(IReadOnlyList<preke> Items)
    {
        foreach (preke item in Items)
        {
            var sooold = _shopDbContext.perkamapreke.Where(x => x.fk_PrekeId == item.id).ToList();
            foreach (perkamapreke i in sooold)
            {
                _shopDbContext.perkamapreke.Remove(i);
            }
        }
        await _shopDbContext.SaveChangesAsync();
    }
}