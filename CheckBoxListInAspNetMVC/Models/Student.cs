using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CheckBoxListInAspNetMVC.Models
{
    public class Student
    {
        public Student()
        {
            Courses = new HashSet<Course>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Student Name")]
        public string Name { get; set; }
        public int? DepartmentId { get; set; }

        [Required]
        public string Email { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}