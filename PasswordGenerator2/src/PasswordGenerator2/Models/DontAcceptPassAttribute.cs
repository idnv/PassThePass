using PasswordGenerator2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PasswordGenerator2.Helpers
{
    /// <summary>
    /// The attribute enforces that the user will enter a password 3 times before it will actually be checked.
    /// </summary>
    public class DontAcceptPassAttribute : ValidationAttribute
    {

        public static Dictionary<string, int> mailToTryCount = new Dictionary<string, int>();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            SignUpStep context = (SignUpStep)(validationContext.ObjectInstance);
            if (context.Pass == null)
            {
                return new ValidationResult("חובה להזין סיסמא");
            }
            if (context.Mail == null)
            {
                return ValidationResult.Success;
            }
            if (!mailToTryCount.ContainsKey(context.Mail))
                mailToTryCount.Add(context.Mail, 0);

            if (++mailToTryCount[context.Mail] < 3)
                return new ValidationResult("הסיסמא אינה חזקה מספיק, אנא נסה סיסמא אחרת.");
            else
            {
                Regex reg = new Regex(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*]).{6,20})");
                if (!reg.IsMatch(context.Pass))
                {
                    return new ValidationResult("הסיסמא חייבת להכיל אותיות קטנות וגדולות, מספרים, וסימן. אורך מינימלי 6 תווים");
                }
                else
                {
                    mailToTryCount.Remove(context.Mail);
                    return ValidationResult.Success;
                }
            }
        }
    }
}