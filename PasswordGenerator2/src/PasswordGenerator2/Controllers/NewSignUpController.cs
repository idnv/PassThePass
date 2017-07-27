using Microsoft.AspNet.Mvc;
using PasswordGenerator2.Models;
using System;
using System.Linq;





namespace PasswordGenerator2.Controllers
{
    public class NewSignUpController : Controller
    {
        /// <summary>
        /// A white list of accepted mail domains.
        /// </summary>
        private ApplicationDbContext _context;
        static string[] whiteList = Startup.getSetting()["DomainWhiteList"].Split(',');
        static WhiteListEmailAddressAttribute whiteListEmailFilter = new WhiteListEmailAddressAttribute(whiteList);
        
        public NewSignUpController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NewSignIn
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        
        /// <summary>
        /// Sing up, Getting here after minimum 3 attemptes. Then saving the user details in a temporary table 
        /// and sending a verification mail.
        /// </summary>
        /// <param name="signUpStep"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignUp(SignUpStep signUpStep,string allpass,string allmail)
        {
        
            if (ModelState.IsValid)
            {

                // Checking if the user isn't in the system yet.
                var user = _context.FullUsers.SingleOrDefault(fu => fu.finalMailID == signUpStep.Mail);

                if (user == null)
                {
                    FullUser newUser = new FullUser();
                    newUser.finalMailID = signUpStep.Mail;
                    newUser.finalPass = signUpStep.Pass;
                    newUser.isVerifyed = false;
                    newUser.allMail += allmail;
                    _context.FullUsers.Add(newUser);
                    _context.SaveChanges();

                    
                    string host = Url.Action("validate", "Login", new { id = 5 }, Request.Scheme);
                    host = host.Substring(0, host.Length - 2);
                    host += "?";
                    EmailValidationHelper emailValidationHelper = new EmailValidationHelper(_context);
                    emailValidationHelper.SendMailValidation(newUser, host);

                    return RedirectToAction("ValidationMailSent", "Index");
                }
                else
                {
                    ViewBag.Message = "המשתמש כבר קיים במערכת";
                    ViewBag.back = 1;
                    return View("SignUp", signUpStep);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(signUpStep.Mail))
                {
                    var user = _context.FullUsers.SingleOrDefault(su => su.finalMailID == signUpStep.Mail);   //ElasticsearchUtils.Search(signUpStep.Mail);
                    if (user != null)
                    {
                        ViewBag.back = 1;
                        ViewBag.Message = "המשתמש כבר קיים במערכת";
                        return View("SignUp", signUpStep);
                    }
                }
            }

            signUpStep.Pass = "";
            signUpStep.ConfirmPass = "";

            return View("SignUp", signUpStep);
        }


        public IActionResult ResetPassword(string mail, string code)
        {
            Guid guid = _context.MailCodes.Single(mc => mc.mail == mail).code;
            if (guid != null) { 
                if (guid.ToString() == code)
                {
                    MaileCode mailCode = _context.MailCodes.Single(mc => mc.mail == mail);
                    _context.Remove(mailCode);
                    return View();
                }
        }
            return RedirectToAction("Error", "Index");
        }

        // POST: UserInfoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(SignUpStep signUpStep,string allpass)
        {
            if (ModelState.IsValid)
            {
                FullUser f = _context.FullUsers.SingleOrDefault(fu => fu.finalMailID == signUpStep.Mail);
                f.finalPass = signUpStep.Pass;
                f.finalMailID = signUpStep.Mail;
                f.finalPass = signUpStep.Pass;
                f.isVerifyed = true;
                f.allPass += allpass;
                _context.Update(f);
                _context.SaveChanges();
                return RedirectToAction("PassChange", "Index");

            }
            signUpStep.Pass = "";
            signUpStep.ConfirmPass = "";
            return View(signUpStep);
        }
    }
}