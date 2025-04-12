namespace WebAppMVCDBFirst.Exceptions;

public class InvalidArgumentException : AppException
{
    private static readonly string DEFAULT_CODE = "Invalid argument.";
    
    public InvalidArgumentException(string code, string message) : base(code + DEFAULT_CODE, message) { }
}