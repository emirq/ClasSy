using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClasSy.Models
{
    [Table("Professors")]
    public class Professor : ApplicationUser
    {
        public byte ClassTeacher { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}