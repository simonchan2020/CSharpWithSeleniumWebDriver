using System;
using CSharpWithSeleniumWebDriver.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CSharpWithSeleniumWebDriver.BasePages
{
    public class GoogleBasePage
    {
        private IWebDriver _driver;
        private SeleniumHelper selenium;

        private By textfieldSearchQuery = By.Name("q");

        public GoogleBasePage(IWebDriver driver)
        {
            _driver = driver;
            selenium = new SeleniumHelper(_driver);
        }

        public void DoSearchFor(string searchQuery)
        {
            selenium.SendKeys(textfieldSearchQuery, searchQuery);

            new Actions(_driver).SendKeys(Keys.Enter).Build().Perform();
        }
    }
}
