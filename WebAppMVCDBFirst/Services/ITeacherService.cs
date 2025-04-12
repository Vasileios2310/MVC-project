using WebAppMVCDBFirst.DTO;
using WebAppMVCDBFirst.Models;

namespace WebAppMVCDBFirst.Services;

public interface ITeacherService
{
    Task SignUpUserAsync(TeacherSignupDTO request);
    Task<List<Users>> GetAllUsersTeachersAsync();
    Task<List<Users>> GetAllUsersTeachersAsync(int pageNumber, int pageSize);
    Task<int> GetUsersTeachersCountAsync();
    Task<Users?> GetTeacherByUsernameAsync(string username);
}