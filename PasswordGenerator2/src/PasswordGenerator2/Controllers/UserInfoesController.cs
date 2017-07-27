using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using PasswordGenerator2.Models;

namespace PasswordGenerator2.Controllers
{
    public class UserInfoesController : Controller
    {
        private ApplicationDbContext _context;

        public UserInfoesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: UserInfoes
        public IActionResult Index()
        {
            return View(_context.UserInfoes.ToList());
        }

        // GET: UserInfoes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            UserInfo userInfo = _context.UserInfoes.Single(m => m.ID == id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }

            return View(userInfo);
        }

        // GET: UserInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserInfoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserInfo userInfo)
        {
            if (ModelState.IsValid && TempData["infoMail"] != null)
            {
                userInfo.mailId = (string)TempData["infoMail"];
                _context.Add(userInfo);
                _context.SaveChanges();
                FullUser f = _context.FullUsers.Single(fu => fu.finalMailID == userInfo.mailId);
                f.infoID = _context.UserInfoes.Single(ui=> ui.mailId == f.finalMailID).ID;
                _context.Update(f);
                _context.SaveChanges();
                // return RedirectToAction("Index", "Login", ls);
                TempData["mail"] = f.finalMailID; ;
                TempData["pass"] = f.finalPass; ;
                return RedirectToAction("Index","Login");
            }
            return View(userInfo);
        }

        // GET: UserInfoes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            UserInfo userInfo = _context.UserInfoes.Single(m => m.ID == id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Update(userInfo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userInfo);
        }


    }
}
