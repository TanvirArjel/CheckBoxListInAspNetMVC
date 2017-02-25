using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CheckBoxListInAspNetMVC.Models
{
    public class Department
    {
        public Department()
        {
            Courses = new HashSet<Course>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        public string Name { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Student> Students { get; set; }

    }
}