using Microsoft.EntityFrameworkCore;
using WebAppMVCDBFirst.Models;

namespace WebAppMVCDBFirst.Repositories;
/// <summary>
/// T class --> not only class , but nullable type (string , enum)
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext dbContext;
    protected readonly DbSet<T> dbSet;
    
    // Encapsulation of DbSet<T> within AppDbContext
    public BaseRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
        dbSet = dbContext.Set<T>();
    }
    
    /// <summary>
    /// Adds a new record , puts the entity in the change tracker with the state Added.
    /// This does not hit the database yet.
    /// When we call dbContext.SaveChangesAsync(), EF will run an INSERT INTO SQL.
    /// </summary>
    /// <param name="entity"></param>
    public virtual async Task AddAsync(T entity) => await dbSet.AddAsync(entity);

    public virtual async Task AddRangeAsync(IEnumerable<T> entities) => await dbSet.AddRangeAsync(entities);

    /// <summary>
    /// Attach(entity) connects the detached object to the context.
    /// Setting State = Modified and tells EF: “This entity exists, but it's been changed — please update it.”
    /// Then, we call SaveChangesAsync() later, and EF executes an UPDATE statement.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual Task UpdateAsync(T entity)
    {
        dbContext.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
      T? existingEntity = await GetAsync(id);
      if(existingEntity is null) 
          return false;
      dbSet.Remove(existingEntity);
      return true;
    }

    public virtual async Task<T?> GetAsync(int id) => await dbSet.FindAsync(id);

    public virtual async Task<IEnumerable<T>> GetAllAsync() => await dbSet.ToListAsync();

    public virtual async Task<int> GetCountAsync() => await dbSet.CountAsync();
}