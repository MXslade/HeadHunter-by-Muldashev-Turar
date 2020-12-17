using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeadHunter2.Models
{
    public class UserProfessionalField
    {
        public int Id { get; set; }

        public int CvId { get; set; }

        public int ProfessionalFieldId { get; set; }

        public CV Cv { get; set; }

        public ProfessionalField ProfessionalField { get; set; }
    }
}