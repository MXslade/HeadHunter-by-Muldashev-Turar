using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HeadHunter2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string FullName { get; set; }

        public ICollection<UserCompany> UserCompanies { get; set; }

        public virtual ICollection<CV> CVs { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<CV> CVs { get; set; }

        public DbSet<Company> Companies{ get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<ProfessionalField> ProfessionalFields { get; set; }

        public DbSet<UserProfessionalField> UserProfessionalFields { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<JobExperience> JobExperiences { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}