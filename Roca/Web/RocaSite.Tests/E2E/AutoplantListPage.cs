using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Protractor;

namespace Cno.Roca.Web.RocaSite.Tests.E2E
{
    public class AutoplantListPage : BasePage
    {
        public AutoplantListPage(NgWebDriver driver, string url) : base(driver, url)
        {
        }

        public AutoplantListPage SelectProject(string projectName)
        {
            NgDriver.FindElement(NgBy.Select("Model.Filter.ProjectId"))
                    .FindElement(ByOptionText(projectName))
                    .Click();
            
            return this;
        }

        public AutoplantListPage SelectArea(string areaName)
        {
            NgDriver.FindElement(NgBy.Select("Model.Filter.AreaId"))
                    .FindElement(ByOptionText(areaName))
                    .Click();

            return this;
        }

        public AutoplantListPage SetPieceMark(string text)
        {
            NgDriver.FindElement(NgBy.Input("Model.Filter.PieceMark"))
                    .SendKeys(text);

            //var wait = new WebDriverWait(NgDriver, TimeSpan.FromSeconds(2));
            //wait.Until(d => d.FindElement(By.ClassName("dropdown-menu")));

            NgDriver.FindElement(NgBy.Input("Model.Filter.PieceMark"))
                    .SendKeys(Keys.ArrowDown + Keys.Enter);

            return this;
        }

        public int GetMaterialsCount()
        {
            var x = NgDriver.FindElements(NgBy.Repeater("material in Model.Materials")).Count;

            return x;

        }

        public string GetMaterialsFieldValue(int index, string field)
        {
            var x = NgDriver.FindElements(NgBy.Repeater("material in Model.Materials"))[index].Evaluate("material." + field) as string;

            return x;

        }
    }
}
