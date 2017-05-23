using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClasSy.Models;

namespace ClasSy.ViewModels
{
    public class StudentViewModel : UserViewModel
    {
        public Student Student { get; set; }    

        public byte ClassPresident { get; set; }

        [Display(Name = "Class")]
        [Required]
        public int SchoolClassId { get; set; }

        public SchoolClass SchoolClass { get; set; }

        public virtual IEnumerable<SchoolClass> SchoolClasses { get; set; }

        public IEnumerable<Student> Students { get; set; }

        public IEnumerable<Course> Courses { get; set; }

        public IEnumerable<Grade> Grades { get; set; }
    }
}