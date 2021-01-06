using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using HeadHunter2.Models;
using HeadHunter2.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
using Syncfusion.Pdf.Grid;
using WebGrease.Css.Extensions;


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
            cvFromDb.About = cv.About;
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

        [HttpPost]
        public ActionResult EditViewType(int view_type, int cv_id)
        {
            var cv = _context.CVs.SingleOrDefault(item => item.Id == cv_id);

            if (cv != null)
            {
                cv.ViewType = view_type;
                _context.SaveChanges();
            }

            return RedirectToAction("Edit", new { Id = cv_id });
        }

        public ActionResult ShowCV(int cv_id)
        {
            var cv = _context.CVs.SingleOrDefault(item => item.Id == cv_id);

            if (cv == null)
            {
                return HttpNotFound();
            }

            return View(cv);
        }

        public ActionResult CreateDocument(int cv_id)
        {
            var cv = _context.CVs.SingleOrDefault(item => item.Id == cv_id);
            if (cv == null)
            {
                return HttpNotFound();
            }

            cv.ApplicationUser = _context.Users.SingleOrDefault(item => item.Id.Equals(cv.ApplicationUserId));
            cv.City = _context.Cities.SingleOrDefault(item => item.Id.Equals(cv.CityId));
            cv.Country = _context.Countries.SingleOrDefault(item => item.Id.Equals(cv.CountryId));

            using (PdfDocument document = new PdfDocument())
            {
                PdfPage page = document.Pages.Add();
                PdfPage page2 = document.Pages.Add();
                PdfPage page3 = document.Pages.Add();

                PdfGraphics graphics = page.Graphics;
                
                PdfFont titleFont = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);
                PdfFont regularTextFont = new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Regular);

                var title = cv.Title;
                var name = "Full Name: " + cv.ApplicationUser.FullName;
                var country = "Country: " + cv.Country.Name;
                var city = "City: " + cv.City.Name;
                var salary = "Salary: " + cv.Salary;
                var aboutWords = cv.About.Split(' ');
                var about = "";
                for (int i = 0; i < aboutWords.Length; ++i)
                {
                    about += aboutWords[i] + " ";
                    if (i % 8 == 0 && i != 0)
                    {
                        about += '\n';
                    }
                }
                var professionalFieldTitle = "Professional Fields";
                var professionalFields = "";

                var list = cv.ProfessionalFields.ToList();
                var counter = 0;

                foreach (var pro in cv.ProfessionalFields)
                {
                    professionalFields += pro.Name + "   ";
                    if (counter % 4 == 0 && counter != 0)
                    {
                        professionalFields += '\n';
                    }

                    counter++;
                }

                var educationTitle = "Education";
                var education = "";
                foreach (var ed in cv.Educations)
                {
                    education += ed.Place + '\n';
                    education += "Faculty: " + ed.Faculty + '\n';
                    education += "Speciality: " + ed.Speciality + '\n';
                    education += ed.Level + '\n';
                    education += "Year of Complition: " + ed.YearOfCompletion + "\n\n\n";
                }

                var experienceTitle = "Job Experience";
                var experience = "";
                foreach (var ex in cv.JobExperiences)
                {
                    experience += ex.CompanyName + '\n';
                    experience += "Position: " + ex.Position + '\n';
                    experience += ex.BeginDate;

                    if (!ex.IsActive)
                    {
                        experience += " - " + ex.EndDate;
                    }

                    experience += '\n';

                    experience += "Duration: " + ex.CountDuration() + "\n\n\n";
                }

                RectangleF titleRectangleF = new RectangleF(PointF.Empty, titleFont.MeasureString(title));
                RectangleF nameRectangleF = new RectangleF(new PointF(titleRectangleF.X, titleRectangleF.Y + titleRectangleF.Height + 20), regularTextFont.MeasureString(name));
                RectangleF countryRectangleF = new RectangleF(new PointF(nameRectangleF.X, nameRectangleF.Y + nameRectangleF.Height + 10), regularTextFont.MeasureString(country));
                RectangleF cityRectangleF = new RectangleF(new PointF(countryRectangleF.X, countryRectangleF.Y + countryRectangleF.Height + 10), regularTextFont.MeasureString(city));
                RectangleF salaryRectangleF = new RectangleF(new PointF(cityRectangleF.X, cityRectangleF.Y + cityRectangleF.Height + 10), regularTextFont.MeasureString(salary));
                RectangleF aboutRectangleF = new RectangleF(new PointF(salaryRectangleF.X, salaryRectangleF.Y + salaryRectangleF.Height + 10), regularTextFont.MeasureString(about));
                RectangleF professionalFieldsTitleRectangleF = new RectangleF(new PointF(aboutRectangleF.X, aboutRectangleF.Y + aboutRectangleF.Height + 20), titleFont.MeasureString(professionalFieldTitle));
                RectangleF professionalFieldsRectangleF = new RectangleF(new PointF(professionalFieldsTitleRectangleF.X, professionalFieldsTitleRectangleF.Y + professionalFieldsTitleRectangleF.Height + 20), regularTextFont.MeasureString(professionalFields));
                RectangleF educationTitleRectangleF = new RectangleF(PointF.Empty, titleFont.MeasureString(educationTitle));
                RectangleF educationRectangleF = new RectangleF(new PointF(educationTitleRectangleF.X, educationTitleRectangleF.Y + educationTitleRectangleF.Height + 10), regularTextFont.MeasureString(education));
                RectangleF experienceTitleRectangleF = new RectangleF(PointF.Empty, titleFont.MeasureString(experienceTitle));
                RectangleF experienceRectangleF = new RectangleF(new PointF(experienceTitleRectangleF.X, experienceTitleRectangleF.Y + experienceTitleRectangleF.Height + 20), regularTextFont.MeasureString(experience));

                graphics.DrawString(title, titleFont, PdfBrushes.Black, titleRectangleF);
                graphics.DrawString(name, regularTextFont, PdfBrushes.Black, nameRectangleF);
                graphics.DrawString(country, regularTextFont, PdfBrushes.Black, countryRectangleF);
                graphics.DrawString(city, regularTextFont, PdfBrushes.Black, cityRectangleF);
                graphics.DrawString(salary, regularTextFont, PdfBrushes.Black, salaryRectangleF);
                graphics.DrawString(about, regularTextFont, PdfBrushes.Black, aboutRectangleF);
                graphics.DrawString(professionalFieldTitle, titleFont, PdfBrushes.Black, professionalFieldsTitleRectangleF);
                graphics.DrawString(professionalFields, regularTextFont, PdfBrushes.Black, professionalFieldsRectangleF);
                page2.Graphics.DrawString(educationTitle, titleFont, PdfBrushes.Black, educationTitleRectangleF);
                page2.Graphics.DrawString(education, regularTextFont, PdfBrushes.Black, educationRectangleF);
                page3.Graphics.DrawString(experienceTitle, titleFont, PdfBrushes.Black, experienceTitleRectangleF);
                page3.Graphics.DrawString(experience, regularTextFont, PdfBrushes.Black, experienceRectangleF);

                document.Save("Output.pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Save);
            }
            throw new NotImplementedException();
        }
    }
}