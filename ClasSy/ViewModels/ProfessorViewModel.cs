using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClasSy.Models;

namespace ClasSy.ViewModels
{
    public class ProfessorViewModel : UserViewModel
    {
        public Professor Professor { get; set; }

        public byte ClassTeacher { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public ICollection<int> SelectedCourseList { get; set; }

        public IEnumerable<Professor> Professors { get; set; }

    }
}