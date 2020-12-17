using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeadHunter2.Models;

namespace HeadHunter2.ViewModels
{
    public class UserCVsViewModel
    {
        public  ApplicationUser User { get; set; }

        public ICollection<CV> CVs { get; set; }
    }
}