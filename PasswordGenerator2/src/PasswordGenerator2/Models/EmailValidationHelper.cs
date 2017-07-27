using PasswordGenerator2.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
//using System.Net.Mail;



namespace PasswordGenerator2
{
    /// <summary>
    /// Manages the token validation stuff
    /// </summary>
    public class EmailValidationHelper
    {
        private ApplicationDbContext _context;
       
        public EmailValidationHelper(ApplicationDbContext context)
        {
            _context = context;
        }
        static string[] whiteList = Startup.getSetting()["DomainWhiteList"].Split(',');
        static WhiteListEmailAddressAttribute whiteListEmailFilter = new WhiteListEmailAddressAttribute(whiteList);
        static readonly string thanks = "תודה על אימות המייל, אנא כנס לקישור המצורף והתחבר לאתר עם פרטיך החדשים";
        static readonly string mailFormat = "{0}" + Environment.NewLine + "<h2><a href='{1}mail={2}&code={3}'>לחץ כאן</a></h2>";

        /// <summary>
        /// Sends the validation token to the given mail.
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="redirect"></param>
        public void SendMailValidation(FullUser fullUser, string redirect)
        {
            var guid = Guid.NewGuid();
            MaileCode mc = new MaileCode(fullUser.finalMailID, guid);

            //check if old request is exist and remove it
            MaileCode existMailCode = _context.MailCodes.SingleOrDefault(mcoode => mcoode.mail == fullUser.finalMailID);
            if (existMailCode != null)
            {
                // remove old request for reset password
                _context.Remove(existMailCode);
            }

            //save new request for reset pass
            _context.MailCodes.Add(mc);
            _context.SaveChanges();



            if (whiteListEmailFilter.IsValid(fullUser.finalMailID))
            {
                SendMsg(redirect,fullUser.finalMailID,guid);
            }
            
        }

        /// <summary>
        /// Actually sending the mail.
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="guid"></param>
        /// <param name="redirect"></param>
        private static void sendMail(string mail, Guid guid, string redirect)
        {
            //using (var client = new SmtpClient("smtp.gmail.com", 587))
            //{
            //    client.Credentials = new NetworkCredential(Startup.getSetting()["Email"], Startup.getSetting()["Pass"]);
            //    client.EnableSsl = true;
            //    MailMessage message = new MailMessage();
            //    message.To.Add(new MailAddress(mail));
            //    message.Subject = thanks;
            //    message.IsBodyHtml = true;
            //    message.Body = string.Format(mailFormat, thanks, redirect, mail, guid.ToString());
            //    client.Send(message);
            //}
        }


        static MailMessage InitMailMessage(string redirect, string Tomail, Guid guid)
        {
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(Tomail));
            message.From = new MailAddress(Startup.getSetting()["Email"], "");
            message.IsBodyHtml = true;
            message.Body = string.Format(mailFormat, thanks, redirect, Tomail, guid.ToString());
            message.Subject = thanks;
            return message;
        }

        static void SendMsg(string redirect, string Tomail, Guid guid)
        {
            MailMessage msg = InitMailMessage(redirect,Tomail,guid);

            SmtpClient smtp = new SmtpClient();
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new System.Net.NetworkCredential(Startup.getSetting()["Email"], Startup.getSetting()["Pass"]);
            smtp.Host = "smtp.gmail.com";
           
                smtp.Send(msg);
            
            
        }
    }
}



