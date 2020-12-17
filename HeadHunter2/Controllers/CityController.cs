using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using HeadHunter2.Models;

namespace HeadHunter2.Controllers
{
    public class CityController : Controller
    {
        private ApplicationDbContext _context;

        public CityController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Edit(int city_id)
        {
            var city = _context.Cities.SingleOrDefault(c => c.Id == city_id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        [HttpPost]
        public ActionResult Edit(City city)
        {
            var cityFromDb = _context.Cities.SingleOrDefault(c => c.Id == city.Id);
            if (cityFromDb == null)
            {
                return HttpNotFound();
            }

            cityFromDb.Name = city.Name;

            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Delete(int city_id)
        {
            var city = _context.Cities.SingleOrDefault(c => c.Id == city_id);
            if (city == null)
            {
                return HttpNotFound();
            }

            _context.Cities.Remove(city);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }
    }
}