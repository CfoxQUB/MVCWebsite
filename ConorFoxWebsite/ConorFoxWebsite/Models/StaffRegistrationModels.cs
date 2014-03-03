using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
namespace ConorFoxWebsite.Models
{
    public class RegisterNewStaffMember
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name="Title")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string Forename { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [EmailAddress(ErrorMessage="This Field contain a valid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }
     
        public string Course { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public DateTime RegisteredDate { get; set; }
    }
}