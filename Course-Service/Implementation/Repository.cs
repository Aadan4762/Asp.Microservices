using Course_Service.Data;
using Course_Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Course_Service.Implementation;


public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>(); // Initialize _dbSet from context
    }
    
    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

   
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync(); 
    }

   
    public async Task UpdateAsync(int id, T entity)
    {
        var existingEntity = await _dbSet.FindAsync(id);
        if (existingEntity == null)
        {
            throw new ArgumentException("Entity not found");
        }
        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            throw new ArgumentException("Entity not found");
        }
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(); 
    }

    // Save changes (Optional, in case you want to call SaveChangesAsync explicitly)
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
