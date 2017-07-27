using PasswordGenerator2.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGenerator2.Models
{
    public class SignUpStep
    {
        public string Code { get; set; }
        [Display(Name = "מייל")]
        [Required(ErrorMessage = "חובה להזין כתובת מייל")]
        [EmailAddress(ErrorMessage = "כתובת מייל אינה תקינה")]
        [WhiteListEmailAddress("walla.com", "walla.co.il", "hotmail.com", "gmail.com", "live.com", "quali.com", "qualisystems.com", "me.com")]
        public string Mail { get; set; }

        [Display(Name = "סיסמא")]
        [Required(ErrorMessage = "חובה להזין סיסמא")]
        [DontAcceptPassAttribute]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        [Display(Name = "אימות סיסמא")]
        [Required(ErrorMessage = "חובה להזין אימות סיסמא")]
        [DataType(DataType.Password)]
        [Compare("Pass", ErrorMessage="הסיסמאות צריכות להיות זהות")]
        public string ConfirmPass { get; set; }

        public int TryCount { get; set; }
    }
}
