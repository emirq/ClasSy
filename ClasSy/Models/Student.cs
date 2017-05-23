using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClasSy.Models
{
    // Author: Lejla Hodžić
    [Table("Students")]
    public class Student : ApplicationUser
    {
        public byte ClassPresident { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }
        public int SchoolClassId { get; set; }
        public virtual ICollection<Parent> Parents { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}