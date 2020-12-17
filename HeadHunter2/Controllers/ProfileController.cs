using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeadHunter2.Models;
using Microsoft.AspNet.Identity;

namespace HeadHunter2.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private ApplicationDbContext _context;

        public ProfileController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
            // GET: Profile
        [Route("Profile")]
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            var user = _context.Users.SingleOrDefault(u => u.Id.Equals(id));
            return View(user);
        }

        public ActionResult AddPhoneNumber(string phoneNumber)
        {
            var id = User.Identity.GetUserId();
            var user = _context.Users.SingleOrDefault(u => u.Id.Equals(id));
            if (user != null)
            {
                user.PhoneNumber = phoneNumber;
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Profile");
        }
    }
}