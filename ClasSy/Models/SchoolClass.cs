using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClasSy.Models
{
    public class SchoolClass
    {
        public int Id { get; set; }
        public string Department { get; set; } // odjeljenje
        public string Name { get; set; }
        public Professor Professor { get; set; } // razrednik
        public string ProfessorId { get; set; }
    }
}