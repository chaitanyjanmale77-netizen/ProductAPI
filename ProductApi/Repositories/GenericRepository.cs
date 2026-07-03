using Microsoft.EntityFrameworkCore;

public class GenericRepository<T> : IGenricRepository<T> where T : class
{
  protected readonly AppDbContext _context;
  public GenericRepository(AppDbContext context){
    _context=context;
  }
    public async Task<List<T>> GetAllAsync()
    {

        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
       return await _context.Set<T>().FindAsync(id);

    }

    public async Task AddAsync(T entity)
    {
         await _context.Set<T>().AddAsync(entity);
         await _context.SaveChangesAsync();
    }
     public async Task UpdateAsync(T entity)
    {
         _context.Set<T>().Update(entity);
         await _context.SaveChangesAsync();
    }

     public async Task DeleteAsync(T entity)
    {
         _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}