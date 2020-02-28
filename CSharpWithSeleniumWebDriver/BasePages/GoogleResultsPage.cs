using System;
using CSharpWithSeleniumWebDriver.Helpers;
using OpenQA.Selenium;

namespace CSharpWithSeleniumWebDriver.BasePages
{
    public class GoogleResultsPage : GoogleBasePage
    {
        private IWebDriver _driver;
        private SeleniumHelper selenium;

        private By textfieldResultStats = By.Id("resultStats");

        public GoogleResultsPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            selenium = new SeleniumHelper(_driver);
        }

        public bool ResultStatsAreDisplayed()
        {
            return selenium.ElementIsVisible(textfieldResultStats);
        }
    }
}
