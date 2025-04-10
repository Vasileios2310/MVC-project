using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAppMVCDBFirst.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // this represents the Course table in database
    public virtual DbSet<Courses> Courses { get; set; }

    public virtual DbSet<Students> Students { get; set; }

    public virtual DbSet<Teachers> Teachers { get; set; }

    public virtual DbSet<Users> Users { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlite();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Courses>(entity =>
        {
            entity.HasMany(d => d.Student).WithMany(p => p.Course)
                .UsingEntity<Dictionary<string, object>>(
                    "CoursesStudents",
                    r => r.HasOne<Students>().WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Courses>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("CourseId", "StudentId");
                        j.HasIndex(new[] { "CourseId" }, "IX_CoursesStudents_CourseId");
                        j.HasIndex(new[] { "StudentId" }, "IX_CoursesStudents_StudentsId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}