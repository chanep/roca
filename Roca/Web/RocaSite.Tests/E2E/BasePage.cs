using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Protractor;

namespace Cno.Roca.Web.RocaSite.Tests.E2E
{
    public abstract class BasePage
    {
        protected NgWebDriver NgDriver { get; set; }

        protected BasePage(NgWebDriver driver, string url)
        {
            NgDriver = driver;
            NgDriver.Navigate().GoToUrl(url);
        }

        protected By ByOptionText(string text)
        {
            return By.XPath(".//option[normalize-space(text())='" + text + "']");
        }

        protected By ByOptionValue(string value)
        {
            return By.XPath(".//option[@value='" + value + "']");
        }
    }
}
