using System.ComponentModel.DataAnnotations;

namespace WebAppMVCDBFirst.DTO;

public class UserLoginDTO
{
    [StringLength(50, MinimumLength = 3 , ErrorMessage = "Username must be between 3 and 50 characters")]
    public string? Username { get; set; }
    
    [RegularExpression(@"(?=.*?[A-Z])(?=.*?[a-z])(?=.*?\d)(?=.*?\W[!@#$%])^.{8,}$", 
        ErrorMessage = "Password must contain at least 8 characters and at least one digit," +
                       " one uppercase letter, one lowercase letter, and one special character")]
    public string? Password { get; set; }
    
    public string? KeepLoggedIn { get; set; }
}