using System.Web.Mvc;
using HeadHunter2.Controllers;
using NUnit.Framework;

namespace NUnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void HomeIndex()
        {
            HomeController contoller = new HomeController();

            ActionResult result = contoller.Index();

            Assert.IsNotNull(result);
        }
    }
}