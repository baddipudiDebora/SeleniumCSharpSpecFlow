using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using BaseProject.PageElementLibrary;
using SeleniumExtras.WaitHelpers;

namespace BaseProject.SeleniumHelpers
{
    public class Synchronisation
    {
        public static void WaitForPageToLoad(double pageWaitTimeout = 120)
        {
            var wait = new WebDriverWait(DriverUtility.webDriver, TimeSpan.FromSeconds(pageWaitTimeout));
            wait.Until(driver1 => ((IJavaScriptExecutor)DriverUtility.webDriver).ExecuteScript("return document.readyState"))
                .Equals("complete");
        }

        public static void WaitUntilAttributeContains(IWebElement element, string propertyName, string value)
        {
            var wait = new WebDriverWait(DriverUtility.webDriver, TimeSpan.FromSeconds(30));
            wait.Until(condition => element.GetAttribute(propertyName).Contains(value));
        }

        public static void WaitUntilAttributeDoesNotContain(IWebElement element, string propertyName, string value)
        {
            var wait = new WebDriverWait(DriverUtility.webDriver, TimeSpan.FromSeconds(30));
            wait.Until(condition => !element.GetAttribute(propertyName).Contains(value));
        }

        public static void HandleStaleElementException(Action action)
        {
            int retries = 0;
            const int max_retries = 20;
            while (retries < max_retries)
            {
                try
                {
                    action();
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    retries++;
                }
            }
        }

        public static void WaitUntilObjectEnabled(IWebElement element)
        {
            var wait = new WebDriverWait(DriverUtility.webDriver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public static void WaitUntilObjectEnabled(By locator)
        {
            var wait = new WebDriverWait(DriverUtility.webDriver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        public static void WaitUntilPopupDisplayed(By locator)
        {
            var wait = new WebDriverWait(DriverUtility.webDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static void WaitUntilObjectDisappears(Element element)
        {
            var locator = element.GetElementByLocator();
            var wait = new WebDriverWait(DriverUtility.webDriver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public static void WaitUntilObjectDisappears(Element element, int timeInSeconds)
        {
            var locator = element.GetElementByLocator();
            var wait = new WebDriverWait(DriverUtility.webDriver, TimeSpan.FromSeconds(timeInSeconds));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public static void WaitUntilObjectAppears(Element element)
        {
            var locator = element.GetElementByLocator();
            var wait = new WebDriverWait(DriverUtility.webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static void WaitUntilObjectAppears(Element element, int timeInSeconds)
        {
            var locator = element.GetElementByLocator();
            var wait = new WebDriverWait(DriverUtility.webDriver, TimeSpan.FromSeconds(timeInSeconds));
            wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static void WaitUntilObjectIsStale(By loadingMaskMessage)
        {
            try
            {
                var wait = new WebDriverWait(DriverUtility.webDriver, TimeSpan.FromSeconds(60));
                wait.Until((d) =>
                {
                    return GetCountOfElementsPresent(loadingMaskMessage).Equals(0);
                });
            }
            catch (Exception)
            {
            }
        }

        public static void WaitUntilObjectIsStale()
        {
            WaitUntilObjectIsStale(By.XPath("//*[contains(@class, 'modal-backdrop')]"));
        }

        public static int GetCountOfElementsPresent(By element)
        {
            DriverUtility.webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            var count = DriverUtility.webDriver.FindElements(element).Count;
            DriverUtility.webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return count;
        }
    }
}