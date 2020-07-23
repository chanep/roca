using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using Protractor;

namespace Cno.Roca.Web.RocaSite.Tests.E2E
{
    public abstract class E2EBaseTest
    {
        protected string BaseUrl { get; set; }

        protected E2EBaseTest()
        {
            BaseUrl = "http://localhost:7806/roca/";
        }

        public virtual NgWebDriver CreateWebDriver()
        {
            var driver = new NgWebDriver(new PhantomJSDriver());
            //var driver = new NgWebDriver(new ChromeDriver());
            return driver;
        }


    }
}
