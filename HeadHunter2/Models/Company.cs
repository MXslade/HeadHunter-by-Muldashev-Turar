using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HeadHunter2.Models
{
    public class Company
    {
        public int Id { get; set; }

        public virtual ICollection<UserCompany> UserCompanies { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "text")] 
        public string Description { get; set; }
    }
}