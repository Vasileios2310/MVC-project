using Microsoft.EntityFrameworkCore;
using WebAppMVCDBFirst.core.enums;
using WebAppMVCDBFirst.Models;

namespace WebAppMVCDBFirst.Repositories;

public class TeacherRepository : BaseRepository<Teachers>, ITeacherRepository
{
    public TeacherRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<Teachers?> GetTeacherByNumberAsync(string phoneNumber)
    {
        return await dbContext.Teachers.Where(t => t.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
    }
    
    public async Task<List<Courses>> GetTeacherCoursesAsync(int Id)
    {
        List<Courses> teachersCoursesList;
        
        teachersCoursesList = await dbContext.Teachers
                                             .Where(t=>t.Id == Id)
                                             .SelectMany(t=>t.Courses)
                                             .ToListAsync();
            return teachersCoursesList;
    }

    public async Task<List<Users>> GetAllUsersTeachersAsync()
    {
        var usersWithTeacherRole = await dbContext.Users
                                                             .Where(u=> u.UserRole == UserRole.Teacher)
                                                             .Include(u => u.Teachers)
                                                             .ToListAsync();
        return usersWithTeacherRole;
    }

    public async Task<List<Users>> GetAllUsersTeachersPaginatedAsync(int pageNumber, int pageSize)
    {
        int skip = pageSize * (pageNumber - 1);
        
        var userWithTeacherRole = await dbContext.Users
                                                         .Where(u => u.UserRole== UserRole.Teacher)
                                                         .Include(u=>u.Teachers)
                                                         .Skip(skip)
                                                         .Take(pageSize)
                                                         .ToListAsync();
        return userWithTeacherRole;
    }
    
    public async Task<PaginatedResult<Users>> GetPaginatedUsersTeachersAsync(int pageNumber, int pageSize)
    {
        var totalRecords = await dbContext.Users.Where(u => u.UserRole ==UserRole.Teacher).CountAsync();
        
        int skip = pageSize * (pageNumber - 1);
        
        var usersWithTeacherRole = await dbContext.Users
            .Where(u => u.UserRole == UserRole.Teacher)
            .Include(u => u.Teachers)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<Users>()
        {
            Data = usersWithTeacherRole,
            TotalRecords = totalRecords,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
    
    public async Task<PaginatedResult<Users>> GetPaginatedUsersTeachersFilteredAsync(int pageNumber, int pageSize, List<Func<Users, bool>> predicates)
    {
        var totalRecords = await dbContext.Users.Where(u => u.UserRole == UserRole.Teacher).CountAsync();
        
        int skip = pageSize * (pageNumber - 1);

        IQueryable<Users> query = dbContext.Users
                                        .Where(u => u.UserRole == UserRole.Teacher)
                                        .Skip(skip)
                                        .Take(pageSize);

        if (predicates != null && predicates.Any())
        {
            query = query.Where(u => predicates.All(predicate => predicate(u)));
        }
        
        var usersTeachers = await query.ToListAsync();

        return new PaginatedResult<Users>()
        {
            Data = usersTeachers,
            TotalRecords = totalRecords,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<Users?> GetUserTeacherByUserNameAsync(string userName)
    {
        var userTeacher = await dbContext.Users
            .Where(u => u.UserRole == UserRole.Teacher && u.Username == userName)
            .SingleOrDefaultAsync();
        
        return userTeacher;
    }
}