using Microsoft.EntityFrameworkCore;
using WebAppMVCDBFirst.core.enums;
using WebAppMVCDBFirst.Models;

namespace WebAppMVCDBFirst.Repositories;

public class StudentRepository : BaseRepository<Students> , IStudentRepository
{
    public StudentRepository(AppDbContext dbContext) : base(dbContext) { }
    
    public async Task<Students?> GetByPersonalNumber(string? perosnalNumber)
    {
        return await dbContext.Students.Where(s => s.PersonalNum == perosnalNumber).SingleOrDefaultAsync();
    }
    
    public async Task<List<Courses>> GetStudentCoursesAsync(int id)
    {
        List<Courses> coursesList;
        
        return coursesList = await dbContext.Students
            .Where(s => s.Id == id)
            .SelectMany(s=>s.Course)
            .ToListAsync();
    }
    
    public async Task<List<Users>> GetAllUsersStudentsAsync()
    {
        return await dbContext.Users
            .Where(u=>u.UserRole == UserRole.Student)
            .Include(u=>u.Students)
            .ToListAsync();
    }
}