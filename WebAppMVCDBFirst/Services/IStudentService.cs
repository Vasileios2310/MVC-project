using WebAppMVCDBFirst.Models;

namespace WebAppMVCDBFirst.Services;

public interface IStudentService
{
    Task<IEnumerable<Users>> GetAllStudentsAsync();
    Task<List<Courses>> GetAllCoursesAsync(int studentId);
    Task<Students?> GetStudentAsync(int studentId);
    Task<bool> DeleteStudentAsync(int studentId);
    Task<int> GetStudentAsync();
}