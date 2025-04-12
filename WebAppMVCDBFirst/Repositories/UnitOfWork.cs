using WebAppMVCDBFirst.Models;

namespace WebAppMVCDBFirst.Repositories;
/// <summary>
/// Each repository interacts directly with the DbContext provided by the UnitOfWork.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public UserRepository UserRepository => new (_dbContext);
    public TeacherRepository TeacherRepository => new (_dbContext);
    public StudentRepository StudentRepository => new (_dbContext);
    public CourseRepository CourseRepository => new (_dbContext);
    
    /// <summary>
    /// Attempts to commit any changes, to the database asynchronously.
    /// It returns true if changes were successfully saved (i.e., the number of rows affected is greater than 0).
    /// If nothing was saved, it returns false
    /// </summary>
    /// <returns></returns>
    public async Task<bool> SaveAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
}