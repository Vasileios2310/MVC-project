using WebAppMVCDBFirst.Models;

namespace WebAppMVCDBFirst.Repositories;

public interface IUserRepository
{
    Task<Users?> GetUserAsync(string username , string password);
    Task<Users?> UpdateUserAsync(int id, Users userDTO);
    Task<Users?> GetByUsernameAsync(string username);
    Task<List<Users>> GetAllUsersFilteredPaginatedAsync(int page, int pageSize,
        List<Func<Users, bool>> predicates);
}