using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HeadHunter2.Models;

namespace HeadHunter2.Controllers
{
    public class EducationApiController : ApiController
    {
        List<Education> educations = new List<Education>();
        public EducationApiController() { }

        public EducationApiController(List<Education> educations)
        {
            this.educations = educations;
        }

        public IEnumerable<Education> GetAllEducations()
        {
            return educations;
        }

        public async Task<IEnumerable<Education>> GetAllEducationsAsync()
        {
            return await Task.FromResult(GetAllEducations());
        }

        public IHttpActionResult GetEducation(int id)
        {
            var education = educations.FirstOrDefault((item) => item.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            return Ok(education);
        }

        public async Task<IHttpActionResult> GetEducationAsync(int id)
        {
            return await Task.FromResult(GetEducation(id));
        }
    }
}
