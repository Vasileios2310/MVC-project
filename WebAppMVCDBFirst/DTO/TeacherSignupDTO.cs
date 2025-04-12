using System.ComponentModel.DataAnnotations;
using WebAppMVCDBFirst.core.enums;

namespace WebAppMVCDBFirst.DTO;

public class TeacherSignupDTO
{
    [StringLength(50, MinimumLength = 3 , ErrorMessage = "Username must be between 3 and 50 characters")]
    public string? Username { get; set; }
    
    [StringLength(100, ErrorMessage = "Email must not exceed 100 characters")]
    [EmailAddress (ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }
    
    [RegularExpression(@"(?=.*?[A-Z])(?=.*?[a-z])(?=.*?\d)(?=.*?\W[!@#$%])^.{8,}$")]
    public string? Password { get; set; }
    
    [StringLength(50, MinimumLength = 3 , ErrorMessage = "Firstname must be between 3 and 50 characters")]
    public string? FirstName { get; set; }
    
    [StringLength(50, MinimumLength = 3 , ErrorMessage = "Lastname must be between 3 and 50 characters")]
    public string? LastName { get; set; }
    
    [StringLength(15, MinimumLength = 10 , ErrorMessage = "Phone number must not exceed 15 characters")]
    public string? PhoneNumber { get; set; }
    
    [StringLength(100, MinimumLength = 2 , ErrorMessage = "Institution must be between 2 and 100 characters")]
    public string? Institution { get; set; }
    
    [EnumDataType(typeof(UserRole),  ErrorMessage = "Invalid user role")]
    public UserRole? UserRole { get; set; }
    
}