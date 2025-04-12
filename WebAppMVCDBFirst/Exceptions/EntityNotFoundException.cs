namespace WebAppMVCDBFirst.Exceptions;

public class EntityNotFoundException : AppException
{
    private static readonly string DEFAULT_CODE = "Entity not found.";

    public EntityNotFoundException(string code, string message) : base(code + DEFAULT_CODE, message) { }
    
}