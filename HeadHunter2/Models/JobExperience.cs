using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Xml.XPath;

namespace HeadHunter2.Models
{
    public class JobExperience : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public int CvId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BeginDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string Position { get; set; }

        public CV Cv { get; set; }

        public string CountDuration()
        {
            DateTime end;
            if (EndDate == null)
            {
                end = DateTime.Now;
            }
            else
            {
                end = (DateTime) EndDate;
            }

            var result = end.Subtract(BeginDate).TotalDays;
            if (result > 365)
            {
                return String.Format("{0:0.00}", result / 365) + " years";
            }
            else
            {
                return String.Format("{0:0.00}", result / 30) + " months";
            }
        }

        public String ShowEndDate()
        {
            DateTime date = EndDate ?? DateTime.Now;
            if (date == DateTime.Now)
            {
                return "";
            }

            return date.ToString("MMMM yyyy");
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (this.Id != 0)
            {
                Validator.TryValidateProperty(this.CvId,
                    new ValidationContext(this, null, null) {MemberName = "CvId"},
                    results);
                Validator.TryValidateProperty(this.CompanyName,
                    new ValidationContext(this, null, null) { MemberName = "CompanyName" },
                    results);
                Validator.TryValidateProperty(this.BeginDate,
                    new ValidationContext(this, null, null) { MemberName = "BeginDate" },
                    results);
                Validator.TryValidateProperty(this.IsActive,
                    new ValidationContext(this, null, null) { MemberName = "IsActive" },
                    results);
                Validator.TryValidateProperty(this.Position,
                    new ValidationContext(this, null, null) { MemberName = "Position" },
                    results);

            }
            if (BeginDate.Year < 1990)
            {
                yield return new ValidationResult("Begin Date should be greater that 1990");
            }
        }
    }
}