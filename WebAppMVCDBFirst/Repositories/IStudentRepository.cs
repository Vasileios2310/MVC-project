using WebAppMVCDBFirst.Models;

namespace WebAppMVCDBFirst.Repositories;

public interface IStudentRepository
{
    Task<List<Courses>> GetStudentCoursesAsync(int id);
    Task<Students?> GetByPersonalNumber(string? perosnalNumber);
    Task<List<Users>> GetAllUsersStudentsAsync();
    
}