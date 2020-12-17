using HeadHunter2.Models;
using HeadHunter2.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HeadHunter2.Controllers
{
    public class EducationController : Controller
    {
        private ApplicationDbContext _context;

        public EducationController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult Create(int cvid)
        {
            var cv = _context.CVs.SingleOrDefault(item => item.Id == cvid);
            if (cv == null)
            {
                return HttpNotFound();
            }
            var education = new Education
            {
                Cv = cv,
                CvId = cvid
            };

            return View(education);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Education education)
        {
            // var education = new Education
            // {
            //     CvId = cvId ?? default(int),
            //     Level = level,
            //     Place = place,
            //     Faculty = faculty,
            //     Speciality = speciality,
            //     YearOfCompletion = yearOfCompletion ?? default(int)
            // };
            if (ModelState.IsValid)
            {
                _context.Educations.Add(education);
                _context.SaveChanges();
                return RedirectToAction("Edit", "CV", new { Id = education.CvId });
            }

            return View(education);
        }

        public ActionResult Edit(int educationId)
        {
            var education = _context.Educations.SingleOrDefault(item => item.Id == educationId);
            if (education == null)
            {
                return HttpNotFound();
            }

            return View(education);
        }

        [HttpPost]
        public ActionResult Edit(Education education)
        {
            if (ModelState.IsValid)
            {
                var educationFromDb = _context.Educations.SingleOrDefault(item => item.Id == education.Id);
                if (educationFromDb == null)
                {
                    return HttpNotFound();
                }

                educationFromDb.Faculty = education.Faculty;
                educationFromDb.YearOfCompletion = education.YearOfCompletion;
                educationFromDb.Place = education.Place;
                educationFromDb.Speciality = education.Speciality;
                educationFromDb.Level = education.Level;

                _context.SaveChanges();

                return RedirectToAction("Edit", "CV", new {Id = education.CvId});
            }

            return View(education);
        }

        public ActionResult Delete(int educationId)
        {
            var education = _context.Educations.SingleOrDefault(item => item.Id == educationId);
            if (education == null)
            {
                return HttpNotFound();
            }

            _context.Educations.Remove(education);
            _context.SaveChanges();

            return RedirectToAction("Edit", "CV", new { Id = education.CvId });
        }
    }
}