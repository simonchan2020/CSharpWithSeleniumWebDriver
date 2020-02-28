using System;
using CSharpWithSeleniumWebDriver.BasePages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CSharpWithSeleniumWebDriver
{
    [TestClass]
    public class GoogleHomePageTests
    {
        IWebDriver driver;

        [TestInitialize]
        public void StartBrowser()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
        }

        [TestMethod]
        public void StartBrowser_PerformGoogleSearch_AssertOnResultPage_StopBrowser()
        {
            new GoogleHomePage(driver)
                .Load()
                .DoSearchFor("Kentucky Fried Chicken");

            new GoogleResultsPage(driver)
                .DoSearchFor("Louisville");
        }

        [TestCleanup]
        public void StopBrowser()
        {
            driver.Quit();
        }
    }
}
