using AutoMapper;
using Serilog;
using WebAppMVCDBFirst.DTO;
using WebAppMVCDBFirst.Exceptions;
using WebAppMVCDBFirst.Models;
using WebAppMVCDBFirst.Repositories;
using WebAppMVCDBFirst.Security;

namespace WebAppMVCDBFirst.Services;

public class TeacherService : ITeacherService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<TeacherService> _logger;
    
    public TeacherService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = new LoggerFactory().AddSerilog().CreateLogger<TeacherService>();
    }
    
    public async Task<List<Users>> GetAllUsersTeachersAsync()
    {
        List<Users> usersTeachers = new();
        try
        {
            usersTeachers = await _unitOfWork.TeacherRepository.GetAllUsersTeachersAsync();
            _logger.LogInformation("{Message}" , "All teachers returned");
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            throw;
        }
        return usersTeachers;
    }
    
    public async Task<List<Users>> GetAllUsersTeachersAsync(int pageNumber, int pageSize)
    {
        List<Users> usersTeachers = new();
        try
        {
            usersTeachers = await _unitOfWork.TeacherRepository.GetAllUsersTeachersPaginatedAsync(pageNumber, pageSize);
            _logger.LogInformation("{Message}" , "All teachers paginated returned");
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            //throw; --> i want to take an empty list , so no " throw" 
        }
        return usersTeachers;
    }
    
    public async Task<Users?> GetTeacherByUsernameAsync(string username)
    {
        return await _unitOfWork.TeacherRepository.GetUserTeacherByUserNameAsync(username);
    }
    
    public async Task<int> GetUsersTeachersCountAsync()
    {
        return await _unitOfWork.TeacherRepository.GetCountAsync();
    }

    public async Task SignUpUserAsync(TeacherSignupDTO request)
    {
        Teachers teacher;
        Users user;

        try
        {
            user = ExtractUserFromDTO(request);
            Users? existingUser = await _unitOfWork.UserRepository.GetByUsernameAsync(user.Username);
            if (existingUser != null)
            {
               throw new EntityAlreadyExistsException("User", "User with username" 
                                                                 + existingUser.Username + "already exists");
            }
            user.Password = EncryptionUtil.Encrypt(user.Password);
            await _unitOfWork.UserRepository.AddAsync(user);
            
            teacher = ExtractTeacherFromDTO(request);
            if (await _unitOfWork.TeacherRepository.GetTeacherByNumberAsync(teacher.PhoneNumber) is not null)
            {
                throw new EntityAlreadyExistsException("Teacher", "Teacher with phone number" 
                                                               + teacher.PhoneNumber + "already exists");
            }
            await _unitOfWork.TeacherRepository.AddAsync(teacher);
            user.Teachers = teacher;
            //teacher.User = user; --> not necessary, because EF manages the other-end relationship, since both entites are attached

            await _unitOfWork.SaveAsync();
            _logger.LogInformation("{Message}" , "User added as a teacher" + teacher + "signed up successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            throw;
        }
    }

    private Users ExtractUserFromDTO(TeacherSignupDTO signupDTO)
    {
        return new Users()
        {
            Username = signupDTO.Username!,
            Password = signupDTO.Password!,
            Email = signupDTO.Email!,
            Firstname = signupDTO.FirstName!,
            Lastname = signupDTO.LastName!,
            UserRole = signupDTO.UserRole,
        };
    }

    private Teachers ExtractTeacherFromDTO(TeacherSignupDTO signupDTO)
    {
        return new Teachers()
        {
            Institution = signupDTO.Institution!,
            PhoneNumber = signupDTO.PhoneNumber!,
        };
    }
}