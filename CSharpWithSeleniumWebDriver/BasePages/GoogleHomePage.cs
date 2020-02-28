using System;
using CSharpWithSeleniumWebDriver.Helpers;
using OpenQA.Selenium;

namespace CSharpWithSeleniumWebDriver.BasePages
{
    public class GoogleHomePage : GoogleBasePage
    {
        private IWebDriver _driver;
        private SeleniumHelper selenium;

        private By textfieldSearchQuery = By.Name("q");
        private By buttonDoSearch = By.Name("btnK");

        public GoogleHomePage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            selenium = new SeleniumHelper(_driver);
        }

        public GoogleHomePage Load()
        {
            selenium.NavigateTo("https://www.google.com");
            return this;
        }

        public GoogleHomePage EnterSearchTerm(String searchQuery)
        {
            selenium.SendKeys(textfieldSearchQuery, searchQuery);
            return this;
        }

        public void ClickSearchButton()
        {
            selenium.Click(buttonDoSearch);
        }

        public void LoadAndSearchFor(String searchQuery)
        {
            Load().EnterSearchTerm(searchQuery).ClickSearchButton();
        }
    }
}
