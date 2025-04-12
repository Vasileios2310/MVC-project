using WebAppMVCDBFirst.core.Filters;
using WebAppMVCDBFirst.DTO;
using WebAppMVCDBFirst.Models;

namespace WebAppMVCDBFirst.Services;

public interface IUserService
{
    Task<Users?> VerifyAndGetUserAsync(UserLoginDTO credentials);
    Task<Users?> GetUserByUserNameAsync(string userName);
    Task<List<Users>> GetAllUsersFilteredAsync(int pageNumber, int pageSize, UserFiltersDTO userFiltersDTO);
    
    
}