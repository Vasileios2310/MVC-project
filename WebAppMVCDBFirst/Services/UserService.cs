using AutoMapper;
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

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
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

    public Task<Users?> GetUserByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<List<Users>> GetAllUsersFilteredAsync(int pageNumber, int pageSize, UserFiltersDTO userFiltersDTO)
    {
        throw new NotImplementedException();
    }
}