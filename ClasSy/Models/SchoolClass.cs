using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClasSy.Models
{
    // Author: Lejla Hodžić
    public class SchoolClass
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string Name { get; set; }
        public Professor Professor { get; set; }
        public string ProfessorId { get; set; }
    }
}