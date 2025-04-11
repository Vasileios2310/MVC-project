using WebAppMVCDBFirst.Models;

namespace WebAppMVCDBFirst.Repositories;

public interface ITeacherRepository
{
    Task<List<Courses>> GetTeacherCoursesAsync(int Id);
    Task<Teachers?> GetTeacherByNumberAsync(string phoneNumber);
    Task<List<Users>> GetAllUsersAsync();
    Task<List<Users>> GetAllUsersTeachersPaginatedAsync(int pageNumber, int pageSize);
    Task<Users?> GetUserTeacherByUserNameAsync(string userName);
    Task<PaginatedResult<Users>> GetPaginatedUsersTeachersAsync(int pageNumber, int pageSize);
    Task<PaginatedResult<Users>> GetPaginatedUsersTeachersFilteredAsync(int pageNumber, int pageSize,List<Func<Users, bool>> predicates);
}