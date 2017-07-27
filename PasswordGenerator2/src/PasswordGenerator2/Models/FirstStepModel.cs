using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Resources;
using System.Text.RegularExpressions;
using System.Linq;

namespace PasswordGenerator2.Models
{
    public class FirstStepModel
    {
        [Display(Name = "הכנס מייל")]
        [Required(ErrorMessage = "חובה להכניס מייל לצורך קבלת קישור אימות")]
        [WhiteListEmailAddress("walla.com", "walla.co.il", "hotmail.com", "gmail.com", "live.com", "quali.com", "qualisystems.com", "me.com")]
        public string Email { get; set; }

    }

  
}