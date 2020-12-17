using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using HeadHunter2.Controllers;
using HeadHunter2.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetAllCvs_ShouldReturnAllCvs()
        {
            var cvs = GetTestCvs();
            var controller = new CvApiController(cvs);

            var result = controller.GetAllCVs() as List<CV>;

            Assert.AreEqual(cvs.Count, result.Count);
        }

        [TestMethod]
        public async Task GetAllCvsAsync_ShouldReturnAllCvs()
        {
            var cvs = GetTestCvs();
            var controller = new CvApiController(cvs);

            var result = await controller.GetAlLCVsAsync() as List<CV>;
            Assert.AreEqual(cvs.Count, result.Count);
        }

        [TestMethod]
        public void GetCv_ShouldReturnCorrectCV()
        {
            var cvs = GetTestCvs();
            var controller = new CvApiController(cvs);

            var result = controller.GetCv(1) as OkNegotiatedContentResult<CV>;
            Assert.IsNotNull(result);
            Assert.AreEqual(cvs[0].Title, result.Content.Title);
        }

        [TestMethod]
        public async Task GetCvAsync_ShouldReturnCorrectProduct()
        {
            var cvs = GetTestCvs();
            var controller = new CvApiController(cvs);

            var result = await controller.GetCvAsync(1) as OkNegotiatedContentResult<CV>;
            Assert.IsNotNull(result);
            Assert.AreEqual(cvs[0].Title, result.Content.Title);
        }

        private List<CV> GetTestCvs()
        {
            var cvs = new List<CV>();

            cvs.Add(new CV(){Id = 1, Title = "Spring Boot Developer", Salary = 100000, About = "The best Spring Boot Developer"});
            cvs.Add(new CV(){Id = 2, Title = "Laravel Developer", Salary = 100000, About = "The best Laravel Developer"});
            cvs.Add(new CV(){Id = 3, Title = ".Net Framework", Salary = 100000, About = "The best .Net Framework Developer"});

            return cvs;
        }

        [TestMethod]
        public void GetAllEducations_ShouldReturnAllEducations()
        {
            var educations = GetTestEducations();
            var controller = new EducationApiController(educations);

            var result = controller.GetAllEducations() as List<Education>;
            Assert.AreEqual(educations.Count, result.Count);
        }

        [TestMethod]
        public async Task GetAllEducationsAsync_ShouldReturnAllEducations()
        {
            var educations = GetTestEducations();
            var controller = new EducationApiController(educations);

            var result = await controller.GetAllEducationsAsync() as List<Education>;
            Assert.AreEqual(educations.Count, result.Count);
        }

        private List<Education> GetTestEducations()
        {
            var educations = new List<Education>();

            educations.Add(new Education() { Id = 1, CvId = 1, Level = "Bachelor", Place = "IITU", Faculty = "KIIT", Speciality = "CSSE", YearOfCompletion = 2022 });
            educations.Add(new Education() { Id = 2, CvId = 1, Level = "Bachelor", Place = "KBRU", Faculty = "KIIT", Speciality = "CSSE", YearOfCompletion = 2022 });
            educations.Add(new Education() { Id = 3, CvId = 1, Level = "Master", Place = "IITU", Faculty = "KIIT", Speciality = "CS", YearOfCompletion = 2024 });

            return educations;
        }
        
    }
}
