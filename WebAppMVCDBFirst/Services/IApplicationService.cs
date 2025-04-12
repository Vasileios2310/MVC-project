namespace WebAppMVCDBFirst.Services;

public interface IApplicationService
{
    UserService UserService { get; }
    TeacherService TeacherService { get; }
    StudentService StudentService { get; }
    // CourseService {get; }    ToDo
}