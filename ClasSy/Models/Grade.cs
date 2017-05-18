using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClasSy.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public byte Value { get; set; }
        public byte Semester { get; set; }
        public Student Student { get; set; }
        public string StudentId { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
    }
}