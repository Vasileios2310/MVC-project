using AutoMapper;
using WebAppMVCDBFirst.Repositories;

namespace WebAppMVCDBFirst.Services;

public class ApplicationService : IApplicationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _ulogger;
    private readonly ILogger<TeacherService> _tlogger;
    private readonly ILogger<StudentService> _slogger;

    public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> ulogger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ulogger = ulogger;
    }

    public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TeacherService> tlogger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tlogger = tlogger;
    }

    public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StudentService> slogger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _slogger = slogger;
    }

    public UserService UserService => new (_unitOfWork , _mapper , _ulogger);
    
    public TeacherService TeacherService => new (_unitOfWork , _mapper , _tlogger);
    
    public StudentService StudentService => new (_unitOfWork , _mapper , _slogger);
}