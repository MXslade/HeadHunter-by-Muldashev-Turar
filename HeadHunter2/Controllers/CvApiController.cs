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
    public class CvApiController : ApiController
    {
        List<CV> cvs = new List<CV>();
        public CvApiController() { }

        public CvApiController(List<CV> cvs)
        {
            this.cvs = cvs;
        }

        public IEnumerable<CV> GetAllCVs()
        {
            return cvs;
        }

        public async Task<IEnumerable<CV>> GetAlLCVsAsync()
        {
            return await Task.FromResult(GetAllCVs());
        }

        public IHttpActionResult GetCv(int id)
        {
            var cv = cvs.FirstOrDefault((item) => item.Id == id);
            if (cv == null)
            {
                return NotFound();
            }

            return Ok(cv);
        }

        public async Task<IHttpActionResult> GetCvAsync(int id)
        {
            return await Task.FromResult(GetCv(id));
        }
    }
}
