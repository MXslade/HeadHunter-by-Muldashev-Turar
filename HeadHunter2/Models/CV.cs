using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DataType(DataType.Text)]
        public string About { get; set; }

        public string Title { get; set; }

        [DefaultValue(0)]  // 0 -> Can be viewed by registered users, 1 -> Can be viewed by the link, 2 -> Open to whole internet
        public int ViewType { get; set; }

        public virtual ICollection<ProfessionalField> ProfessionalFields { get; set; }

        public virtual ICollection<Education> Educations { get; set; }

        public virtual ICollection<JobExperience> JobExperiences { get; set; }
    }
}