using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAppMVCDBFirst.Models;

[Index("Institution", Name = "IX_Teachers_Institution")]
[Index("UserId", Name = "IX_Teachers_UserId", IsUnique = true)]
public partial class Teachers
{
    [Key]
    public int Id { get; set; }

    public string Institution { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int? UserId { get; set; }

    [InverseProperty("Teacher")]
    public virtual ICollection<Courses> Courses { get; set; } = new List<Courses>();

    [ForeignKey("UserId")]
    [InverseProperty("Teachers")]
    public virtual Users? User { get; set; }
}
