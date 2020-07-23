using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Protractor;

namespace Cno.Roca.Web.RocaSite.Tests.E2E
{
    [TestClass]
    public class AutoplantTest : E2EBaseTest
    {
        private NgWebDriver _driver;
        private string _url;
        private AutoplantListPage _page; 

        [TestInitialize]
        public void Initialize()
        {
            _driver = CreateWebDriver();
            _driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 0, 60));
            _url = BaseUrl + "#/Autoplant/List";
            _page = new AutoplantListPage(_driver, _url);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void ShouldFilterByProject()
        {
            _page.SelectProject("Es El Chorrillo");
        }

        [TestMethod]
        public void ShouldFilterByArea()
        {
            _page.SelectArea("Todas");
        }

        [TestMethod]
        public void ShouldFilterByPieceMark()
        {
            _page.SelectProject("Es El Chorrillo")
                 .SelectArea("Todas")
                 .SetPieceMark("P12789273");

            int count = _page.GetMaterialsCount();

            Assert.IsTrue(count > 0, "Should return almost one Material and was " + count);

            for (int i = 0; i < count; i++)
            {
                var pieceMark = _page.GetMaterialsFieldValue(i, "PieceMark");
                Assert.AreEqual("P12789273", pieceMark);
            }

        }
    }
}
