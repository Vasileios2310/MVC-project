using WebAppMVCDBFirst.Models;

namespace WebAppMVCDBFirst.Repositories;

public interface ICourseRepository
{
    Task<List<Students>> GetCourseStudentsAsync(int id);
    Task<Teachers?> GetCourseTeacherAsync(int id);
}