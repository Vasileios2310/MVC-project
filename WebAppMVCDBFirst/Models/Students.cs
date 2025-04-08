using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAppMVCDBFirst.Models;

[Index("Institution", Name = "IX_Students_Institution")]
[Index("PersonalNum", Name = "IX_Students_PersonalNum", IsUnique = true)]
[Index("UserId", Name = "IX_Students_UserId", IsUnique = true)]
public partial class Students
{
    [Key]
    public int Id { get; set; }

    public string PersonalNum { get; set; } = null!;

    public string Institution { get; set; } = null!;

    public byte[]? Department { get; set; }

    public int? UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Students")]
    public virtual Users? User { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("Student")]
    public virtual ICollection<Courses> Course { get; set; } = new List<Courses>();
}
