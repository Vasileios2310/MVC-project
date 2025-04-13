using AutoMapper;
using Serilog;
using WebAppMVCDBFirst.core.Filters;
using WebAppMVCDBFirst.DTO;
using WebAppMVCDBFirst.Models;
using WebAppMVCDBFirst.Repositories;

namespace WebAppMVCDBFirst.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = new LoggerFactory().AddSerilog().CreateLogger<UserService>();
    }

    public async Task<Users?> VerifyAndGetUserAsync(UserLoginDTO credentials)
    {
        Users? user;
        try
        {
            user = await _unitOfWork.UserRepository.GetUserAsync(credentials.Username! , credentials.Password!);
            _logger.LogInformation($"User {credentials.Username} logged in.");
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}{Exception}" , ex.Message , ex.StackTrace);
            throw;
        }
        return user;
    }

    public async Task<Users?> GetUserByUserNameAsync(string userName)
    {
        Users? user = null;
        try
        {
            user = await _unitOfWork.UserRepository.GetByUsernameAsync(userName);
            _logger.LogInformation($"User {userName} is found.");
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}{Exception}", e.Message, e.StackTrace);
        }
        return user;
    }

    public async Task<List<Users>> GetAllUsersFilteredAsync(int pageNumber, int pageSize, UserFiltersDTO userFiltersDTO)
    {
        List<Users> users = new();
        List<Func<Users, bool>> predicates = new();

        try
        {
            if (!string.IsNullOrEmpty(userFiltersDTO.Username))
            {
                predicates.Add(u => u.Username == userFiltersDTO.Username);
            }
            if (!string.IsNullOrEmpty(userFiltersDTO.Email))
            {
                predicates.Add(u => u.Email == userFiltersDTO.Email);
            }
            if (!string.IsNullOrEmpty(userFiltersDTO.UserRole))
            {
                predicates.Add(u=> u.UserRole.ToString() == userFiltersDTO.UserRole);
            }
            users = await _unitOfWork.UserRepository.GetAllUsersFilteredPaginatedAsync(pageNumber, pageSize, predicates);
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}{Exception}", ex.Message , ex.StackTrace);
            throw;
        }
        return users;
    }
}