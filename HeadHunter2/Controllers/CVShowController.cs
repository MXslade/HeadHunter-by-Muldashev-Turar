using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeadHunter2.Models;
using Microsoft.Ajax.Utilities;

namespace HeadHunter2.Controllers
{
    public class CVShowController : Controller
    {
        private ApplicationDbContext _context;

        public CVShowController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index(int cv_id)
        {
            var cv = _context.CVs.SingleOrDefault(item => item.Id == cv_id);

            if (cv == null)
            {
                return HttpNotFound();
            }

            var user = _context.Users.SingleOrDefault(item => item.Id.Equals(cv.ApplicationUserId));
            if (user == null)
            {
                return HttpNotFound();
            }

            cv.ApplicationUser = user;
            cv.Country = _context.Countries.SingleOrDefault(item => item.Id == cv.CountryId);
            cv.City = _context.Cities.SingleOrDefault(item => item.Id == cv.CityId);

            if (cv.ViewType == 0 && !User.Identity.IsAuthenticated)
            {
                return HttpNotFound();
            }

            return View(cv);
        }
    }
}