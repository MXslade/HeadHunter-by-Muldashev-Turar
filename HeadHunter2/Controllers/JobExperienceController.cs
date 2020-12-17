using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeadHunter2.Models;
using HeadHunter2.ViewModels;

namespace HeadHunter2.Controllers
{
    public class JobExperienceController : Controller
    {
        private ApplicationDbContext _context;

        public JobExperienceController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Edit(int jobExperienceId)
        {
            var jobExperience = _context.JobExperiences.SingleOrDefault(item => item.Id == jobExperienceId);
            if (jobExperience == null)
            {
                return HttpNotFound();
            }

            return View(jobExperience);
        }

        [HttpPost]
        public ActionResult Edit(JobExperience jobExperience)
        {
            if (ModelState.IsValid)
            {
                var jobExperienceFromDb = _context.JobExperiences.SingleOrDefault(item => item.Id == jobExperience.Id);
                if (jobExperienceFromDb == null)
                {
                    return HttpNotFound();
                }

                jobExperienceFromDb.BeginDate = jobExperience.BeginDate;
                jobExperienceFromDb.CompanyName = jobExperience.CompanyName;
                jobExperienceFromDb.EndDate = jobExperience.EndDate;
                jobExperienceFromDb.Position = jobExperience.Position;
                jobExperienceFromDb.IsActive = jobExperience.IsActive;

                _context.SaveChanges();

                return RedirectToAction("Edit", "CV", new {Id = jobExperience.CvId});
            }

            return View(jobExperience);
        }

        public ActionResult Delete(int jobExperienceId)
        {
            var jobExperience = _context.JobExperiences.SingleOrDefault(item => item.Id == jobExperienceId);
            if (jobExperience == null)
            {
                return HttpNotFound();
            }

            _context.JobExperiences.Remove(jobExperience);
            _context.SaveChanges();

            return RedirectToAction("Edit", "CV", new { Id = jobExperienceId});
        }

        public ActionResult Create(int cvId)
        {
            var cv = _context.CVs.SingleOrDefault(item => item.Id == cvId);

            if (cv == null)
            {
                return HttpNotFound();
            }

            var jobExperience = new JobExperience
            {
                CvId = cvId,
                Cv = cv
            };

            return View(jobExperience);
        }

        [HttpPost]
        public ActionResult Create(JobExperience jobExperience)
        {
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(jobExperience, new ValidationContext(jobExperience, null, null), results))
            {
                if (results.Count > 0)
                {
                    return View(jobExperience);
                }
                var cv = _context.CVs.SingleOrDefault(item => item.Id == jobExperience.CvId);
                if (cv == null)
                {
                    return HttpNotFound();
                }

                _context.JobExperiences.Add(jobExperience);
                _context.SaveChanges();

                return RedirectToAction("Edit", "CV", new {Id = jobExperience.CvId});
            }

            return View(jobExperience);
        }
    }
}