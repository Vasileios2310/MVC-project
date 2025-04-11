using Microsoft.EntityFrameworkCore;
using WebAppMVCDBFirst.Models;
using WebAppMVCDBFirst.Security;

namespace WebAppMVCDBFirst.Repositories;

public class UserRepository : BaseRepository<Users>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<Users?> GetUserAsync(string username, string password)
    {
        return await dbContext.Users
            .FirstOrDefaultAsync(u=> (u.Username == username || u.Email == username) 
                                     && EncryptionUtil.IsValidPassword(password,u.Password));
    }

    public async Task<Users?> UpdateUserAsync(int id, Users user)
    {
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u=>u.Id== user.Id);
        if(existingUser is null) return null;
        if(existingUser.Id != id) return null;
        
        dbContext.Users.Attach(user);
        dbContext.Entry(user).State = EntityState.Modified;
        return existingUser;
    }

    public async Task<Users?> GetByUsernameAsync(string username) => await dbContext.Users
                                                                .FirstOrDefaultAsync(u => u.Username == username);
    
    public async Task<List<Users>> GetAllUsersFilteredPaginatedAsync(int page, int pageSize, List<Func<Users, bool>> predicates)
    {
        int skip = (page - 1) * pageSize;
        IQueryable<Users> query = dbContext.Users.Skip(skip).Take(pageSize);

        if (predicates != null && predicates.Any())
        {
            // Keep only the users where all the given predicates,  return true for that user.
            query = query.Where(u=> predicates.All(predicate => predicate(u)));
        }
        return await query.ToListAsync();
    }
}