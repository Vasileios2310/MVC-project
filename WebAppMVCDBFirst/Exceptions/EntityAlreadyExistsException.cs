namespace WebAppMVCDBFirst.Exceptions;

public class EntityAlreadyExistsException : AppException
{
    private static readonly string DEFAULT_CODE = "Entity already exists.";

    public EntityAlreadyExistsException(string code, string message) : base(code + DEFAULT_CODE, message) { }
    
}