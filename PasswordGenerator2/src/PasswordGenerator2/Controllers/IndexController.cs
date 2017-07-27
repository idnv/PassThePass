  using Microsoft.AspNet.Mvc;
using PasswordGenerator2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;

namespace PasswordGenerator2.Controllers
{
    public class IndexController : Controller
    {
        static string[] whiteList = Startup.getSetting()["DomainWhiteList"].Split(',');
        static WhiteListEmailAddressAttribute whiteListEmailFilter = new WhiteListEmailAddressAttribute(whiteList);
        //static readonly string thanks = "תודה על אימות המייל, אנא כנס ולחץ על הקישור להמשך";
        //static readonly string mailFormat = "{0}" + Environment.NewLine + "<h2><a href='{1}{2}mail={3}&code={4}'>לחץ כאן</a></h2>";
        //static readonly string LoginPath = "/signin/index?";
        //static Nest.ElasticClient db = new Nest.ElasticClient(new Nest.ConnectionSettings(new Uri(ConfigurationManager.AppSettings["ElasticServer"])));
        private ApplicationDbContext _context;

        public IndexController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Index
        public ActionResult Index()
        {
            return View("Error");
        }

        /// <summary>
        /// Displays a message about the validation ail being sent.
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidationMailSent()
        {
            return View();    
        }

        /// <summary>
        /// The thanks view
        /// </summary>
        /// <returns></returns>
        public ActionResult Thanks()
        {
            return View();
        }

        public ActionResult PassChange()
        {
            return View();
        }

        public IActionResult Test()
        {
            if (TempData["infoMail"] == null)
                return View("Error");
                       
            ViewBag.mail = TempData["infoMail"];
            return View("TypeTest");

            
        }


        public ActionResult TypeTest(string mail, int num_Of_WriitenWords, int correct_Words, float Accuracy, int num_Of_Entry_Errors,decimal time)
        {
            FullUser user = _context.FullUsers.SingleOrDefault(fu => fu.finalMailID == mail);
            user.needTest = false;
            AvgRes res_avg = _context.AvgReses.SingleOrDefault(ra => ra.mail == mail);

            // first time doing test
            if (res_avg == null)
            {
                res_avg = new AvgRes();
                res_avg.mail = mail;
                _context.AvgReses.Add(res_avg);
                _context.SaveChanges();
            }

            res_avg.current_count++;
            if (res_avg.current_count == 1)
            {
                res_avg.current_correctWords = correct_Words;
                res_avg.current_numOfWriitenWords = num_Of_WriitenWords;
                res_avg.current_accuracy = Accuracy;
                res_avg.current_numOfEntryErrors = num_Of_Entry_Errors;
                res_avg.current_time = time;

                res_avg.previous_correctWords = 0;
                res_avg.previous_numOfWriitenWords = 0;
                res_avg.previous_accuracy = 0;
                res_avg.previous_numOfEntryErrors = 0;
                res_avg.previous_time = 0;
            }

            else
            {
                res_avg.previous_correctWords = res_avg.current_correctWords;
                res_avg.previous_numOfWriitenWords = res_avg.current_numOfWriitenWords;
                res_avg.previous_accuracy = res_avg.current_accuracy;
                res_avg.previous_numOfEntryErrors = res_avg.current_numOfEntryErrors;
                res_avg.previous_time = res_avg.current_time;

                res_avg.current_correctWords = ((res_avg.current_correctWords * (res_avg.previous_count)) + correct_Words) / res_avg.current_count;
                res_avg.current_numOfWriitenWords = ((res_avg.current_numOfWriitenWords * (res_avg.previous_count)) + num_Of_WriitenWords) / res_avg.current_count;
                res_avg.current_accuracy = ((res_avg.current_accuracy * (res_avg.previous_count)) + Accuracy) / res_avg.current_count;
                res_avg.current_numOfEntryErrors = ((res_avg.current_numOfEntryErrors * (res_avg.previous_count)) + num_Of_Entry_Errors) / res_avg.current_count;
                res_avg.current_time = ((res_avg.current_time * (res_avg.previous_count)) + time) / res_avg.current_count;


            }

            res_avg.previous_count++;
            _context.Update(res_avg);
            user.resAvgID = res_avg.ID;
            _context.Update(user);
            _context.SaveChanges();
            AvgRes model = _context.AvgReses.SingleOrDefault(ra => ra.mail == mail);
            UserInfo users= _context.UserInfoes.SingleOrDefault(ua => ua.mailId == mail);
            ViewBag.Message = users.message;
            return View("Thanks",model);


        }

        public ActionResult Error(string eror)
        {
            if (eror != null)
                ViewBag.Message = eror;
            return View();


       //     if (TempData["message"] != null)
       //         ViewBag.Message = TempData["message"];
         //   return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(FirstStepModel model)
        {
            /*
            var isValid = whiteListEmailFilter.IsValid(model.Email);
            if (!isValid)
                ViewBag.Message = "למייל לא תקין, אנא נסה שנית";
            else
            {
                var guid = Guid.NewGuid();
                //break to user and mail domin to index the registration key
                var userNDomain = model.Email.Split('@');
                var domain = userNDomain.Last().Split('.').First();
                var user = userNDomain.First();
                
                //index registration key, it will override old key
                var result = db.Raw.Index("domains", domain, user, new { code = guid.ToString() });
                if (result.Success)
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("donotreplayonlinetest@gmail.com");
                    message.To.Add(new MailAddress(model.Email));
                    message.Subject = thanks;
                    message.IsBodyHtml = true;
                    var host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                    message.Body = string.Format(mailFormat, thanks, host, LoginPath, model.Email, guid.ToString());
                    using (SmtpClient smtpClient = new SmtpClient())
                        smtpClient.Send(message);
                    ViewBag.Message = "נשלח המייל לאישור כתובת הדואר האלקטרונית, כנס למייל להמשך הרשמה";
                }
                else
                {
                    ViewBag.Message = "חלה שגיאה בהרשמה, אנא נסה שנית";
                }

            }*/
            return View();
        }
    }
}