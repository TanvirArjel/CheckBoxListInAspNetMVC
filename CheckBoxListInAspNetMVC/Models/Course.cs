using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CheckBoxListInAspNetMVC.Models
{
    public class Course
    {
        public Course()
        {
            Departments = new HashSet<Department>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        public string Name { get; set; }
        public virtual  Department Department { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Department> Departments { get; set; }

    }
}