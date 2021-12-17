using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using BaseProject.SeleniumHelpers;
using FluentAssertions;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace BaseProject.PageElementLibrary
{
    public class Element
    {
        public string Identifier;
        public string Locator;

        public Element(string locatorParam, string identifierParam)
        {
            Identifier = identifierParam;
            Locator = locatorParam;
        }

        public IWebElement GetElement()
        {
            By byObj = DriverUtility.GetElementByLocatorAndIdentifier(Locator, Identifier);
            byObj.Should().NotBeNull();

            return DriverUtility.GetElementObjectFromByLocator(byObj);
        }

        public IWebElement GetElement(IWebElement ancestor)
        {
            return DriverUtility.GetElementFromParentObject(ancestor, DriverUtility.GetElementByLocatorAndIdentifier(Locator, Identifier));
        }

        public By GetElementByLocator()
        {
            By byObj = DriverUtility.GetElementByLocatorAndIdentifier(Locator, Identifier);
            byObj.Should().NotBeNull();

            return byObj;
        }

        public IReadOnlyCollection<IWebElement> GetElements()
        {
            By byObj = DriverUtility.GetElementByLocatorAndIdentifier(Locator, Identifier);
            byObj.Should().NotBeNull();

            return DriverUtility.GetElementCollectionFromLocator(byObj);
        }

        public IReadOnlyCollection<IWebElement> GetElements(IWebElement ancestor)
        {
            return DriverUtility.GetElementCollectionFromParentObject(ancestor, DriverUtility.GetElementByLocatorAndIdentifier(Locator, Identifier));
        }

        public bool GetElementExistsStatus()
        {
            if (GetElementCount() > 0)
            {
                return true;
            }

            return false;
        }

        public bool GetElementExistsStatus(IWebElement ancestor)
        {
            if (this.GetElements(ancestor).Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal int GetElementCount()
        {
            int count = GetElements().Count;
            return count;
        }

        public string GetElementAttributeValue(string attribute)
        {
            var returnType = GetElement().GetAttribute(attribute);
            return returnType;
        }

        public void ClickElement()
        {
            var wait = new WebDriverWait(DriverUtility.webDriver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementToBeClickable(GetElementByLocator()));
            GetElement().Click();
        }

        public string GetElementTextValue()
        {
            return GetElement().Text;
        }

        public string GetElementTextValue(IWebElement ancestor)
        {
            return GetElement(ancestor).GetAttribute("innerText");
        }

        public void DoubleClickElement()
        {
            Actions builder = new Actions(DriverUtility.webDriver);
            var element = GetElement();
            builder.DoubleClick(element).Build().Perform();
        }

        public void ClickHiddenElement()
        {
            IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)DriverUtility.webDriver;
            
            scriptExecutor.ExecuteScript("arguments[0].click();", GetElement());
        }
        
        public IWebElement GetElementFromExistingObject(IWebElement parentObj)
        {
            return DriverUtility.GetElementFromParentObject(parentObj, DriverUtility.GetElementByLocatorAndIdentifier(Locator, Identifier));
        }

        public IWebElement GetElementFromExistingObject(IWebElement parentObj, string locator, string identifier)
        {
            return DriverUtility.GetElementFromParentObject(parentObj, DriverUtility.GetElementByLocatorAndIdentifier(locator, identifier));
        }

        public IReadOnlyCollection<IWebElement> GetElementCollectionFromExistingObject(IWebElement parentObj)
        {
            return DriverUtility.GetElementCollectionFromParentObject(parentObj, DriverUtility.GetElementByLocatorAndIdentifier(Locator, Identifier));
        }

        public IReadOnlyCollection<IWebElement> GetElementCollectionFromExistingObject(IWebElement parentObj, By byObj)
        {
            return DriverUtility.GetElementCollectionFromParentObject(parentObj, byObj);
        }

        public static void ClickItemFromCollection(List<IWebElement> collection, string attribute, string itemToClick)
        {
            var elementToClick = collection.FirstOrDefault(item => item.GetAttribute(attribute) == itemToClick);
            elementToClick.Should().NotBeNull();
            elementToClick.Click();
        }

        public bool IsDisplayed()
        {
            return IsDisplayed(null);
        }

        public bool IsDisplayed(IWebElement ancestor)
        {
            // Since this solution specifies an implicit wait in DriverUtility.SetupBrowser() method, using an 
            // explicit wait is NOT recommended as mixing implicit and explicit waits can cause unpredictable 
            // wait times (see https://www.selenium.dev/documentation/en/webdriver/waits/)
            TimeSpan originalWait = DriverUtility.webDriver.Manage().Timeouts().ImplicitWait;

            try
            {
                DriverUtility.webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

                bool exists;

                if (ancestor == null)
                {
                    exists = this.GetElementExistsStatus();
                }
                else
                {
                    exists = this.GetElementExistsStatus(ancestor);
                }

                if (exists)
                {
                    bool displayed;

                    if (ancestor == null)
                    {
                        displayed = this.GetElement().Displayed;
                    }
                    else
                    {
                        displayed = this.GetElement(ancestor).Displayed;
                    }

                    if (displayed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                DriverUtility.webDriver.Manage().Timeouts().ImplicitWait = originalWait;
            }

        }
    }
}
