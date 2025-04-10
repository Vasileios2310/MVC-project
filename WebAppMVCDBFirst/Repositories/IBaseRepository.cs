namespace WebAppMVCDBFirst.Repositories;
/// <summary>
/// Defines a contract for a repository that handles operations for entities of type T.
/// Basic CRUD operations
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBaseRepository<T>
{
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<T?> GetAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<int> GetCountAsync();
}