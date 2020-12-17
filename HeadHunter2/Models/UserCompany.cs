using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HeadHunter2.Models
{
    public class UserCompany
    {
        [Key, Column(Order = 1)]
        public string ApplicationUserId { get; set; }

        [Key, Column(Order = 2)]
        public int CompanyId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public Company Company { get; set; }
    }
}