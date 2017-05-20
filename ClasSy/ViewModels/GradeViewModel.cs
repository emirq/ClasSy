using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClasSy.Models;

namespace ClasSy.ViewModels
{
    public class GradeViewModel
    {
        [Range(1, 5)]
        public byte Value { get; set; }

        public byte Semester { get; set; }

        public Student Student { get; set; }

        public string StudentId { get; set; }

        public Course Course { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }

        public Professor Professor { get; set; }

        public string ProfessorId { get; set; }

        public IList<Course> ProfessorCourses { get; set; }
    }
}