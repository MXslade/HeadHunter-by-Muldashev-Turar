using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using HeadHunter2.Models;
using HeadHunter2.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

namespace HeadHunter2.Controllers
{
    [Authorize]
    public class CVController : Controller
    {
        private ApplicationDbContext _context;

        public CVController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: CV
        [Route("MyCVs")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.SingleOrDefault(u => u.Id.Equals(userId));
            if (user != null)
            {
                var viewModel = new UserCVsViewModel
                {
                    User = user,
                    CVs = user.CVs
                };
                return View(viewModel);
            }
            return HttpNotFound();
        }

        [Route("AddCV")]
        public ActionResult New()
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.SingleOrDefault(u => u.Id.Equals(userId));
            if (user != null)
            {
                var viewModel = new CVFormViewModel
                {
                    User = user,
                    Cities = _context.Cities.ToList(),
                    Countries = _context.Countries.ToList()
                };
                return View(viewModel);
            }
            throw new NotImplementedException();
        }

        [Route("AddCV")]
        [HttpPost]
        public ActionResult Create(CV cv)
        {
            _context.CVs.Add(cv);
            _context.SaveChanges();

            return RedirectToAction("Index", "CV");
        }

        [Route("MyCVs/Edit/{Id}")]
        public ActionResult Edit(int Id)
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.SingleOrDefault(u => u.Id.Equals(userId));
            if (user != null)
            {
                var cv = user.CVs.SingleOrDefault(item => item.Id == Id);
                if (cv == null)
                {
                    return HttpNotFound();
                }
                var viewModel = new CVFormViewModel
                {
                    User = user,
                    Cities = _context.Cities.ToList(),
                    Countries = _context.Countries.ToList(),
                    ProfessionalFields = _context.ProfessionalFields.ToList().FindAll(item => !cv.ProfessionalFields.Contains(item)),
                    CV = cv
                };
                return View(viewModel);
            }
            return RedirectToAction("Index", "CV");
        }

        [HttpPost]
        public ActionResult SaveEdit(CV cv)
        {
            var cvFromDb = _context.CVs.SingleOrDefault(c => c.Id == cv.Id);
            cvFromDb.Title = cv.Title;
            cvFromDb.Salary = cv.Salary;
            cvFromDb.CityId = cv.CityId;
            cvFromDb.City = cv.City;
            cvFromDb.CountryId = cv.CountryId;
            cvFromDb.Country = cv.Country;
            _context.SaveChanges();
            return RedirectToAction("Index", "CV");
        }

        public ActionResult Delete(int Id)
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.SingleOrDefault(u => u.Id.Equals(userId));
            if (user != null)
            {
                var cvs = _context.CVs.ToList();
                foreach (CV cv in cvs)
                {
                    if (cv.Id == Id && cv.ApplicationUserId.Equals(userId))
                    {
                        _context.CVs.Remove(cv);
                        _context.SaveChanges();
                        return RedirectToAction("Index", "CV");
                    }
                }
            }
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult AddToCv(int cv_id, int professional_field_id)
        {
            var cv = _context.CVs.SingleOrDefault(item => item.Id == cv_id);
            var professionalField = _context.ProfessionalFields.SingleOrDefault(item => item.Id == professional_field_id);

            if (cv == null || professionalField == null)
            {
                return HttpNotFound();
            }

            cv.ProfessionalFields.Add(professionalField);
            professionalField.CVs.Add(cv);

            _context.SaveChanges();

            return RedirectToAction("Edit", new {Id = cv_id});
        }

        [HttpPost]
        public ActionResult RemoveFromCv(int cv_id, int professional_field_id)
        {
            var cv = _context.CVs.SingleOrDefault(item => item.Id == cv_id);
            var professionalField = _context.ProfessionalFields.SingleOrDefault(item => item.Id == professional_field_id);

            if (cv == null || professionalField == null)
            {
                return HttpNotFound();
            }

            cv.ProfessionalFields.Remove(professionalField);
            professionalField.CVs.Remove(cv);

            _context.SaveChanges();

            return RedirectToAction("Edit", new { Id = cv_id });
        }
    }
}