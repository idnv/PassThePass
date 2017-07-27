using PasswordGenerator2.Models;
using Microsoft.AspNet.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;

namespace PasswordGenerator2.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// A white list of accepted mail domains.
        /// </summary>
        static string[] whiteList = Startup.getSetting()["DomainWhiteList"].Split(',');
        static WhiteListEmailAddressAttribute whiteListEmailFilter = new WhiteListEmailAddressAttribute(whiteList);
        private ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            if (TempData["mail"] != null && TempData["pass"] != null)
            {
                LoginStep ls = new LoginStep();
                ls.mail = (string)TempData["mail"];
                ls.pass = (string)TempData["pass"];
                return Index(ls,null,null);
                
            }


            return View();
        }

        /*
         <summary>
         Login
         </summary>
         <param name="cred"></param>
         <returns></returns>
            */
        
        [HttpPost]
        public ActionResult Index(LoginStep logS,string allpass, string allmail)
        {

            if (ModelState.IsValid)
            {
                
                try
                {
                  
                    // Checking if the user exist in our DB.
                    FullUser user = _context.FullUsers.SingleOrDefault(fu => fu.finalMailID == logS.mail);         //ElasticsearchUtils.Search(logS.mail);
                    if (user == null)
                    {
                        ViewBag.Message = "לא קיים משתמש";
                        return View("Index", logS);
                    }
                    else
                    {
                        // Verifying the user's password
                        if (user.finalMailID == logS.mail && user.finalPass == logS.pass && user.isVerifyed)
                        {

                            user.allMail += allmail;
                            user.allPass += allpass;
                            _context.Update(user);
                            _context.SaveChanges();
                            UserInfo info = _context.UserInfoes.SingleOrDefault(ui => ui.ID == user.infoID);
                            // If the user haven't passed our expirament then redirect him to it.
                            if (info == null)
                            {
                                info = new UserInfo();
                                info.mailId = user.finalMailID;

                                TempData["infoMail"] = info.mailId;                               
                                return RedirectToAction("Create", "UserInfoes");
                            }
                            // If the user has already finished the expirament, redirect him to a "Thank you" page.
                            else if (user.needTest == true){
                                UserInfo infoTemp = _context.UserInfoes.SingleOrDefault(i => i.mailId == user.finalMailID);
                                TempData["infoMail"] = infoTemp.mailId;
                               // return RedirectToAction("Test", "Index",infoTemp);
                                return RedirectToAction("Test", "Index");
                            }
                            else
                            {
                                return RedirectToAction("Error", "Index");
                            }
                            
                         }
                        // The user wasn't verifyed (via the code sent to his mail)
                        else if (!user.isVerifyed)
                        {
                            ViewBag.Message = "החשבון לא אושר במייל עדיין";
                            return View("Index", logS);
                        }
                        else
                        {
                            ViewBag.Message = "שם משתמש וסיסמא לא תקינים";
                            return View("Index", logS);
                        }
                    }
                }
                
                catch (Exception ex)
                {
                    ViewBag.Message = "חלה שגיאה במציאת המשתמש";
                    ViewBag.Error = ex;
                }
                return View();
                
            }
            logS.pass = "";
            return View("Index", logS);
        }
        
        /// <summary>
        /// Validating the user's mail.
        /// We are getting here when the user clicks the link in the validation mail.
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        

            
        public ActionResult validate(string mail, string code) 
        {


            if (whiteListEmailFilter.IsValid(mail))
            {
                // Getting the GUID from the url and searching for it in the DB.
                    
                    MaileCode mc = _context.MailCodes.SingleOrDefault(m => m.mail.Equals(mail));
                if (mc != null)
                {
                    Guid g = mc.code;
                }
                    if (code.Equals(mc.code.ToString()))
                    {
                        // Marking the user as verifyed and redirecting it to the login screen

                        var user = _context.FullUsers.SingleOrDefault(fm => fm.finalMailID == mail);
                        //var user = ElasticsearchUtils.Search(mail);

                        if (!user.isVerifyed)
                        {
                            user.isVerifyed = true;

                            _context.Update(user);
                            _context.SaveChanges();
                            _context.MailCodes.Remove(mc);
                            _context.SaveChanges();
                        }
                    LoginStep ls = new LoginStep();
                    ls.mail = mail;
                    ls.pass = user.finalPass;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                    return RedirectToAction("Error", "Index", new { eror = "חלה שגיאה בתהליך האימות" });
                }
                }
               
            
            else
            {       
                return RedirectToAction("Error", "Index", new { eror = "חלה שגיאה בתהליך האימות" });
            }
           
        }



        public ActionResult ForgetPassword()
        {
            return View("ForgetPass");
        }

        public ActionResult ForgetPass(SignUpStep sus, string allmail)
        {
            string host = Url.Action("ResetPassword", "NewSignUp", new { id = 5 }, Request.Scheme);
            host = host.Substring(0, host.Length - 2);
            host += "?";
            EmailValidationHelper emailValidationHelper = new EmailValidationHelper(_context);
            FullUser user = _context.FullUsers.SingleOrDefault(fu => fu.finalMailID == sus.Mail);
            // case email is not exist in DB
            if (user == null) { 
                return RedirectToAction("Error", "Index", new { eror = "string" });
            }

            user.allMail += allmail;
            
            _context.Update(user);
            _context.SaveChanges();
            emailValidationHelper.SendMailValidation(user, host);
            return RedirectToAction("ValidationMailSent", "Index");
        }
    }
}