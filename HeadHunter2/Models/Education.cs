using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HeadHunter2.Models
{
    public class Education
    {
        public int Id { get; set; }

        [Required]
        public int CvId { get; set; }

        [Required]
        public string Level { get; set; }

        [Required]
        public string Place { get; set; }

        public string Faculty { get; set; }

        public string Speciality { get; set; }

        [Required]
        [Range(0, 9999)]
        public int YearOfCompletion { get; set; }

        public CV Cv { get; set; }
    }
}