using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClasSy.Models;

namespace ClasSy.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual IEnumerable<Professor> Professors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}