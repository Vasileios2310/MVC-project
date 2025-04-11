using Microsoft.EntityFrameworkCore;
using WebAppMVCDBFirst.Models;

namespace WebAppMVCDBFirst.Repositories;

public class CourseRepository : BaseRepository<Courses>,ICourseRepository
{
    public CourseRepository(AppDbContext dbContext) : base(dbContext) { }
    
    public async Task<List<Students>> GetCourseStudentsAsync(int id)
    {
        return await dbContext.Courses
            .Where(c => c.Id == id)
            .SelectMany(c => c.Student)
            .ToListAsync();
    }

    public async Task<Teachers?> GetCourseTeacherAsync(int id)
    {
        var course = await dbContext.Courses
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        
        return course?.Teacher;
    }
}