using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeadHunter2.Models;
using HeadHunter2.ViewModels;

namespace HeadHunter2.Controllers
{
    public class ProfessionalFieldController : Controller
    {
        private ApplicationDbContext _context;

        public ProfessionalFieldController()
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

        public ActionResult Create(ProfessionalField professionalField)
        {
            _context.ProfessionalFields.Add(professionalField);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Edit(int professional_field_id)
        {
            var professionalField = _context.ProfessionalFields.SingleOrDefault(p => p.Id == professional_field_id);
            if (professionalField == null)
            {
                return HttpNotFound();
            }

            return View(professionalField);
        }

        [HttpPost]
        public ActionResult Edit(ProfessionalField professionalField)
        {
            var professionalFieldFromDb = _context.ProfessionalFields.SingleOrDefault(p => p.Id == professionalField.Id);
            if (professionalFieldFromDb == null)
            {
                return HttpNotFound();
            }

            professionalFieldFromDb.Name = professionalField.Name;

            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Delete(int professional_field_id)
        {
            var professionalField = _context.ProfessionalFields.SingleOrDefault(p => p.Id == professional_field_id);
            if (professionalField == null)
            {
                return HttpNotFound();
            }

            _context.ProfessionalFields.Remove(professionalField);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }
    }
}