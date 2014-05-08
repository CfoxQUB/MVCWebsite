using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace ConorFoxWebsite.Models
{
    /// <summary>
    /// Logon Model used in staff an student logon page
    /// </summary>
    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

   
    /// <summary>
    /// UserModel passed into teh views
    /// to provide information on user
    /// </summary>
    public class UserModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        public string Forename { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Role { get; set; }
        
        [Required]
        public int Id { get; set; }

    }
    
    /// <summary>
    /// Timetable model used to populate fullcalander display
    /// </summary>
    public class TimetableDisplayModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public bool AllDay { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

    }
    
    /// <summary>
    /// Invites table display model 
    /// </summary>
    public class InvitesModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Start { get; set; }

        public string Duration { get; set; }

        public string Time { get; set; }

    }
}
