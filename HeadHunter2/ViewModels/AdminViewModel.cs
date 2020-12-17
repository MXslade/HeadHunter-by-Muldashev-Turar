using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeadHunter2.Models;

namespace HeadHunter2.ViewModels
{
    public class AdminViewModel
    {
        public ICollection<City> Cities { get; set; }

        public ICollection<Country> Countries { get; set; }

        public ICollection<ProfessionalField> ProfessionalFields { get; set; }

    }
}