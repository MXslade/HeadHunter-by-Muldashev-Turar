using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeadHunter2.Models;

namespace HeadHunter2.ViewModels
{
    public class CVFormViewModel
    {
        public CV CV { get; set; }
        
        public List<City> Cities { get; set; }

        public List<Country> Countries { get; set; }

        public ICollection<ProfessionalField> ProfessionalFields { get; set; }

        public ApplicationUser User { get; set; }
    }
}