using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClasSy.Models;

namespace ClasSy.ViewModels
{
    public class ParentViewModel : UserViewModel
    {
        public ICollection<Parent> Parents { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public ICollection<string> SelectedStudentList { get; set; }
        public Parent Parent;
    }
}