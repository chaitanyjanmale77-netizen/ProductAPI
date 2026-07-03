using Microsoft.EntityFrameworkCore;

public class ProductRepository
    : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<List<Product>> GetAllAsync(QueryParameter qs)
    {
        var query= _context.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(qs.search))
        {
                query = query.Where(p =>
                    p.Name.Contains(qs.search));
        }
        return await _context.Products.Skip((qs.PageNumber-1) * qs.PageSize).Take(qs.PageSize).ToListAsync();
    }
}