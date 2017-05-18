using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClasSy.Models
{
    [Table("Parents")]
    public class Parent : ApplicationUser
    {
        public virtual IEnumerable<Student> Students { get; set; }
    }
}