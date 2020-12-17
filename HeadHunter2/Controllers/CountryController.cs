using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeadHunter2.Models;

namespace HeadHunter2.Controllers
{
    public class CountryController : Controller
    {
        private ApplicationDbContext _context;

        public CountryController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("Country/New")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Edit(int country_id)
        {
            var country = _context.Countries.SingleOrDefault(c => c.Id == country_id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        [HttpPost]
        public ActionResult Edit(Country country)
        {
            var countryFromDb = _context.Countries.SingleOrDefault(c => c.Id == country.Id);
            if (countryFromDb == null)
            {
                return HttpNotFound();
            }

            countryFromDb.Name = country.Name;

            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Delete(int country_id)
        {
            var country = _context.Countries.SingleOrDefault(c => c.Id == country_id);
            if (country != null)
            {
                _context.Countries.Remove(country);
                _context.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }

            return HttpNotFound();
        }
    }
}