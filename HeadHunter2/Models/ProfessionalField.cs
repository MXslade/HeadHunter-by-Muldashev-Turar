using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeadHunter2.Models
{

    public class ProfessionalField
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CV> CVs { get; set; }

    }
}