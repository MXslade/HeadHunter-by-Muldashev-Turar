using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HeadHunter2.Models
{
    public class CV
    {
        public int Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        
        public string ApplicationUserId { get; set; }

        public City City { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }

        public Country Country { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public double Salary { get; set; }

        public string About { get; set; }

        public string Title { get; set; }

        public virtual ICollection<ProfessionalField> ProfessionalFields { get; set; }

        public virtual ICollection<Education> Educations { get; set; }

        public virtual ICollection<JobExperience> JobExperiences { get; set; }
    }
}