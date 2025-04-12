using AutoMapper;
using WebAppMVCDBFirst.Models;
using WebAppMVCDBFirst.Repositories;

namespace WebAppMVCDBFirst.Services;

public class StudentService : IStudentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<StudentService> _logger;

    public StudentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StudentService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<Users>> GetAllStudentsAsync()
    {
       List<Users> userStudents;
       try
       {
           userStudents = await _unitOfWork.StudentRepository.GetAllUsersStudentsAsync();
       }
       catch (Exception ex)
       {
           _logger.LogError("{Message}{Exception}" , ex.Message, ex.StackTrace);
           throw;
       }
       return userStudents;
    }

    public async Task<List<Courses>> GetAllCoursesAsync(int studentId)
    {
        List<Courses> courses = new List<Courses>();

        try
        {
            courses = await _unitOfWork.StudentRepository.GetStudentCoursesAsync(studentId);
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            throw;
        }
        return courses;
    }

    public async Task<Students?> GetStudentAsync(int studentId)
    {
        Students student = null;
        try
        {
            student = await _unitOfWork.StudentRepository.GetAsync(studentId);
            _logger.LogInformation("Student {StudentId}" , student.Id);
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}{Exception}", e.Message, e.StackTrace);
            throw;
        }
        return student;
    }

    public async Task<bool> DeleteStudentAsync(int studentId)
    {
        bool studentDeleted = false;

        try
        {
            studentDeleted = await _unitOfWork.StudentRepository.DeleteAsync(studentId);
            _logger.LogInformation("{Message}" , "Student with Id: " + studentId + " has been deleted");
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            throw;
        }
        return studentDeleted;
    }

    public async Task<int> GetStudentAsync()
    {
        int count;
        try
        {
            count = await _unitOfWork.StudentRepository.GetCountAsync();
            _logger.LogInformation("Student {StudentId}" , count);
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            throw;
        }
        return count;
    }
}