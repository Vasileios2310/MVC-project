using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebAppMVCDBFirst.core.enums;

namespace WebAppMVCDBFirst.Models;

[Index("Email", Name = "IX_Users_Email")]
[Index("Username", Name = "IX_Users_Username")]
public partial class Users
{
    [Key]
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    // correct string --> UserRole
    public UserRole? UserRole { get; set; } = null!;

    [InverseProperty("User")]
    public virtual Students? Students { get; set; }

    [InverseProperty("User")]
    public virtual Teachers? Teachers { get; set; }
}
