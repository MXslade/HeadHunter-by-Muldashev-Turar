using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeadHunter2.Models;
using HeadHunter2.ViewModels;

namespace HeadHunter2.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        
        [Route("Admin")]
        public ActionResult Index()
        {
            var viewModel = new AdminViewModel
            {
                Cities = _context.Cities.ToList(),
                Countries = _context.Countries.ToList(),
                ProfessionalFields = _context.ProfessionalFields.ToList()
            };
            return View(viewModel);
        }
    }
}
