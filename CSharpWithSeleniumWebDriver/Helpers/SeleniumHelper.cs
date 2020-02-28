using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CSharpWithSeleniumWebDriver.Helpers
{
    public class SeleniumHelper
    {
        private IWebDriver _driver;

        public SeleniumHelper(IWebDriver driver)
        {
            _driver = driver;
        }

        public void NavigateTo(String url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Tries to send the given input string to the element specified taking into account the predefined timeout
        ///  Catches and handles exceptions that might occur
        /// </summary>
        /// <param name="by"></param>
        /// <param name="valueToType"></param>
        public void SendKeys(By by, string valueToType)
        {
            try
            {
                new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(by));
                _driver.FindElement(by).Clear();
                _driver.FindElement(by).SendKeys(valueToType);
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                Assert.Fail($"Exception occurred in SeleniumHelper.SendKeys(): element located by {by.ToString()} could not be located within 10 seconds.");
            }
            catch (Exception ex) when (ex is StaleElementReferenceException)
            {
                // find element again and retry
                new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(by));
                _driver.FindElement(by).Clear();
                _driver.FindElement(by).SendKeys(valueToType);
            }
        }

        public bool TitleIsEqualTo(string expectedTitle)
        {
            try
            {
                new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.TitleIs(expectedTitle));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void Select(By by, string valueToSelect)
        {
            try
            {
                new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(by));
                new SelectElement(_driver.FindElement(by)).SelectByText(valueToSelect);
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail($"Exception occurred in SeleniumHelper.Select(): element located by {by.ToString()} could not be located within 10 seconds.");
            }
            catch (NoSuchElementException)
            {
                Assert.Fail($"Exception occurred in SeleniumHelper.Select(): option {valueToSelect} could not be selected.");
            }
        }

        /// <summary>
        /// Waits for an element to be clickable (visible AND enabled)
        /// Takes into account a predefined timeout
        /// Preferred method to be used for determining whether a page has been loaded
        /// See for example all Page Object constructors
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>        
        public bool WaitForElementOnPageLoad(By by)
        {
            try
            {
                new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(by));
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Tries to click an element taking into account a pred
        /// This can generate a variety of exception that are all handled in this methodefined timeout
        /// </summary>
        /// <param name="by"></param>
        public void Click(By by)
        {
            try
            {
                new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(by));
                _driver.FindElement(by).Click();
            }
            catch (Exception ex) when (ex is WebDriverTimeoutException || ex is NoSuchElementException)
            {
                Assert.Fail($"Exception occurred in SeleniumHelper.Click(): element located by {by.ToString()} could not be located within 10 seconds.");
            }
        }

        /// <summary>
        /// Returns the value of the text property for the specified element
        /// Mostly used for retrieving values for input elements (text boxes)
        /// Catches and handles exceptions that might occur
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public string GetElementText(By by)
        {
            string returnValue = "";

            try
            {
                new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(by));
                returnValue = _driver.FindElement(by).Text;
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                Assert.Fail($"Exception occurred in SeleniumHelper.GetElementText(): element located by {by.ToString()} could not be located within 10 seconds.");
            }

            return returnValue;
        }

        /// <summary>
        /// Returns whether an element is visible
        /// Takes into account a predefined timeout
        /// Logs to HTML if the element is not present and visible after this timeout
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public bool ElementIsVisible(By by)
        {
            try
            {
                new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible((by)));
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                return false;
            }
            return true;
        }

    }
}
