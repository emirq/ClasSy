using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClasSy.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birthdate")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Birthplace")]
        public string BirthPlace { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}