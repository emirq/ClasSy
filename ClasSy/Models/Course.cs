using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClasSy.Models
{
    // Author: Emir Kurtanović
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // property which creates resource relationship
        public virtual ICollection<Professor> Professors { get; set; }
        public IEnumerable<Grade> Grades { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}