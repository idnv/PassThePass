using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGenerator2.Models
{
    public class LoginStep
    {
        [Display(Name="מייל")]
        [Required(ErrorMessage = "חובה להזין כתובת מייל")]
        [EmailAddress(ErrorMessage = "כתובת מייל אינה תקינה")]
        public string mail { get; set; }
        
        [Display(Name="סיסמא")]
        [Required(ErrorMessage = "חובה להזין סיסמא")]
        [DataType(DataType.Password)]
        public string pass { get; set; }
    }
}
