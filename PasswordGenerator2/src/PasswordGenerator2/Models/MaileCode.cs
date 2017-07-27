using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordGenerator2.Models
{
    public class MaileCode
    {
        public int ID { get; set; }
        public string mail { get; set; }
        public Guid  code { get; set; }

        public MaileCode()
        {
            mail = String.Empty;
            code = Guid.Empty;
        }

        public MaileCode(string m, Guid c)
        {
            mail = m ;
            code = c;
        }
    }
}
