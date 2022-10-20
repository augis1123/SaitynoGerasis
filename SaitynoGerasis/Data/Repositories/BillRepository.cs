using SaitynoGerasis.Data.Entities;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using System.Security.Cryptography.Xml;

namespace SaitynoGerasis.Data.Repositories;

public interface IBillRepository
{
    Task<saskaita?> GetAsync(int billId);
    Task<IReadOnlyList<saskaita>> GetManyAsync(IReadOnlyList<perkamapreke> perkamaprekes);
    Task CreateAsync(saskaita bill);
    Task UpdateAsync(saskaita bill);
    Task DeleteAsync(saskaita bill);
}

public class BillRepository : IBillRepository
{
    private readonly ShopDbContext _shopDbContext;

    public BillRepository(ShopDbContext shopDbContext)
    {
        _shopDbContext = shopDbContext;
    }

    public async Task<saskaita?> GetAsync(int billId)
    {
        return await _shopDbContext.Saskaita.FirstOrDefaultAsync(o => o.Id == billId);
    }

    public async Task<IReadOnlyList<saskaita>> GetManyAsync(IReadOnlyList<perkamapreke> perkamaprekes)
    {

        List<int> ids = new List<int>();
        foreach (var item in perkamaprekes)
        {
            ids.Add(item.fk_SaskaitaId);
        }

        return await _shopDbContext.Saskaita.Where(o => ids.Contains(o.Id)).ToListAsync();
    }


    public async Task CreateAsync(saskaita bill)
    {
        _shopDbContext.Saskaita.Add(bill);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(saskaita bill)
    {
        _shopDbContext.Saskaita.Update(bill);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(saskaita bill)
    {
        _shopDbContext.Saskaita.Remove(bill);
        await _shopDbContext.SaveChangesAsync();
    }
}