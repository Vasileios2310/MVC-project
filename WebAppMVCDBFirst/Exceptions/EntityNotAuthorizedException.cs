namespace WebAppMVCDBFirst.Exceptions;

public class EntityNotAuthorizedException : AppException
{
    private static readonly string DEFAULT_CODE = "Entity not authorized.";

    public EntityNotAuthorizedException(string code, string message) : base(code +DEFAULT_CODE, message) { }
}