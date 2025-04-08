using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAppMVCDBFirst.Models;

[Index("Description", Name = "IX_Students_Description")]
public partial class Courses
{
    [Key]
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int? TeacherId { get; set; }

    [ForeignKey("TeacherId")]
    [InverseProperty("Courses")]
    public virtual Teachers? Teacher { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Course")]
    public virtual ICollection<Students> Student { get; set; } = new List<Students>();
}
