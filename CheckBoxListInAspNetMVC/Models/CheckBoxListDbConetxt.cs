using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CheckBoxListInAspNetMVC.Models
{
    public class CheckBoxListDbConetxt : DbContext
    {
        public CheckBoxListDbConetxt() : base("CheckBoxListConnection")
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                 .HasMany(c => c.Courses).WithMany(i => i.Students)
                .Map(t => t.MapLeftKey("StudentId")
                .MapRightKey("CourseId")
                .ToTable("StudentCourse"));

            modelBuilder.Entity<Department>()
                 .HasMany(c => c.Courses).WithMany(i => i.Departments)
                .Map(t => t.MapLeftKey("DepartmentId")
                .MapRightKey("CourseId")
                .ToTable("DepartmentCourse"));
        }

    }
}