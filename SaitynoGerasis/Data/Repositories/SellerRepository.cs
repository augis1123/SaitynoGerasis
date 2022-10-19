using SaitynoGerasis.Data.Entities;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace SaitynoGerasis.Data.Repositories;

public interface ISellerRepository
{
    Task<pardavejas?> GetAsync(int sellerId);
    Task<IReadOnlyList<pardavejas>> GetManyAsync();
    Task CreateAsync(pardavejas seller);
    Task UpdateAsync(pardavejas seller);
    Task DeleteAsync(pardavejas seller);
}

public class SellerRepository : ISellerRepository
{
    private readonly ShopDbContext _shopDbContext;

    public SellerRepository(ShopDbContext shopDbContext)
    {
        _shopDbContext = shopDbContext;
    }

    public async Task<pardavejas?> GetAsync(int sellerId)
    {
        return await _shopDbContext.Pardavejas.FirstOrDefaultAsync(o => o.id == sellerId);
    }

    public async Task<IReadOnlyList<pardavejas>> GetManyAsync()
    {
        return await _shopDbContext.Pardavejas.ToListAsync();
    }


    public async Task CreateAsync(pardavejas seller)
    {
        _shopDbContext.Pardavejas.Add(seller);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(pardavejas seller)
    {
        _shopDbContext.Pardavejas.Update(seller);
        await _shopDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(pardavejas seller)
    {
        _shopDbContext.Pardavejas.Remove(seller);
        await _shopDbContext.SaveChangesAsync();
    }
}