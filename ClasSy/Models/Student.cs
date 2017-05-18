using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClasSy.Models
{
    [Table("Students")]
    public class Student : ApplicationUser
    {
        public byte ClassPresident { get; set; }
        public SchoolClass SchoolClass { get; set; }
        public int SchoolClassId { get; set; }
        public virtual IEnumerable<Parent> Parents { get; set; }
    }
}