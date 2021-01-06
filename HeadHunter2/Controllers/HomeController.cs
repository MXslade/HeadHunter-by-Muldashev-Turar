using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeadHunter2.Models;

namespace HeadHunter2.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var allCVs = _context.CVs.ToList();
            var res = new List<CV>();

            foreach (var cv in allCVs)
            {
                if (cv.ViewType == 2 || (User.Identity.IsAuthenticated && cv.ViewType == 0))
                {
                    cv.ApplicationUser = _context.Users.SingleOrDefault(item => item.Id.Equals(cv.ApplicationUserId));
                    res.Add(cv);
                }
            }

            return View(res);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}