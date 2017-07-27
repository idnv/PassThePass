using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordGenerator2.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class WhiteListEmailAddressAttribute : DataTypeAttribute
    {
        HashSet<string> _whiteList;
        public WhiteListEmailAddressAttribute(params string[] whiteList)
            : base(DataType.EmailAddress)
        {
            _whiteList = new HashSet<string>(whiteList);
            // DevDiv 468241: set DefaultErrorMessage not ErrorMessage, allowing user to set
            // ErrorMessageResourceType and ErrorMessageResourceName to use localized messages.
            this.ErrorMessage = string.Format("המייל חייב להיות תקין, וגם להיות מאחד השרותים הבאים: {0}", string.Join(",", whiteList));
        }

        public override bool IsValid(object value)
        {
            string valueAsString = value as string;

            if (string.IsNullOrWhiteSpace(valueAsString))
                return false;
            var splited = valueAsString.Split('@');

            var domain = splited.LastOrDefault();
            if (string.IsNullOrWhiteSpace(domain))
                return false;

            var user = splited.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(user))
                return false;

            return _whiteList.Contains(domain);
        }
    }
}
