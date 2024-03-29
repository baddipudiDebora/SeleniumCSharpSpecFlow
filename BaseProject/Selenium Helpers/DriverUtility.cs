﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FluentAssertions;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using System.Drawing;

namespace BaseProject.SeleniumHelpers
{
    public class DriverUtility
    {
        public static IWebDriver webDriver;

        #region WebDriver Browser Actions
    
        internal static void SetupBrowser(string browserType, double timeoutInSeconds)
        {
            browserType.Should().NotBeNullOrEmpty();
            timeoutInSeconds.Should().NotBe(-1);

            switch (browserType.Trim().ToLower())
            {
                case "chrome":
                    webDriver = new ChromeDriver();
                    break;
                case "ie":
                    webDriver = new InternetExplorerDriver();
                    break;
                case "firefox":
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
                    service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                    webDriver = new FirefoxDriver(service);
                    break;
                case "edge":
                    break;
                default:
                    throw new Exception("Unhandled browser name : " + browserType);
            }

            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            webDriver.Manage().Window.Maximize();
        }

   
        internal static void NavigateToURL(string url)
        {
            url.Should().NotBeNullOrEmpty();
            webDriver.Navigate().GoToUrl(url);
        }

        internal static void NavigateForwardOnBrowser()
        {
            webDriver.Navigate().Forward();
        }

        internal static void MaximiseBrowserWindow()
        {
            webDriver.Manage().Window.Maximize();
        }

        public static void CloseBrowser()
        {
            if (webDriver != null)
            {
                webDriver.Close();
                webDriver.Quit();
            }
        }

        public static string GetBrowserTitle()
        {
            return webDriver.Title;
        }

        public static string GetBrowserURL()
        {
            return webDriver.Url;
        }

        public static void NavigateBackOnBrowser()
        {
            webDriver.Navigate().Back();
        }

        public static void CloseBrowserTab()
        {
            webDriver.Close();
        }

        public static void SwitchToTab(string url)
        {
            var handles = webDriver.WindowHandles.ToList();
            var handleForTab = handles.FirstOrDefault(item => webDriver.SwitchTo().Window(item).Url == url);
            webDriver.SwitchTo().Window(handleForTab);
        }

        public static void RefreshBrowser()
        {
            var url = webDriver.Url;
            webDriver.Navigate().Refresh();
            Synchronisation.WaitForPageToLoad();
            var newURL = webDriver.Url;
            url.Should().BeEquivalentTo(newURL);
        }

        #endregion

        #region WebElement Actions

        internal static By GetElementByLocatorAndIdentifier(string locator, string identifierValue)
        {
            By elementLocatorBy = null;
            Type classType = GetClassTypeFromWebDriverAssembly("By");
            MethodInfo method = GetMethodFromClass(classType, locator);
            elementLocatorBy = (By)method.Invoke(null, new[] { identifierValue });
            return elementLocatorBy;
        }

        internal static IWebElement GetElementObjectFromByLocator(By byObj)
        {
            try
            {
                return webDriver.FindElement(byObj);
            }
            catch
            {
                return null;
                throw new Exception("Unable to find object with By Locator: " + byObj.ToString());
            }
        }

        internal static IWebElement GetElementFromParentObject(IWebElement parentObj, By byObj)
        {
            try
            {
                return parentObj.FindElement(byObj);
            }
            catch (Exception)
            {
                return null;
                throw new Exception("Unable to find object with By Locator: " + byObj.ToString());
            }
        }

        internal static IReadOnlyCollection<IWebElement> GetElementCollectionFromParentObject(IWebElement parentObj, By byObj)
        {
            try
            {
                return parentObj.FindElements(byObj);
            }
            catch (Exception)
            {
                return null;
                throw new Exception("Unable to find object with By Locator: " + byObj.ToString());
            }
        }

        internal static IReadOnlyCollection<IWebElement> GetElementCollectionFromLocator(By byObj)
        {
            try
            {
                return webDriver.FindElements(byObj);
            }
            catch (Exception)
            {
                return null;
                throw new Exception("Unable to find element collection with By Locator: " + byObj.ToString());
            }
        }

        public static void MoveMouseTo(IWebElement webElement)
        {
            new Actions(webDriver).MoveToElement(webElement).Perform();
        }

        public static string CaptureScreenWithExtent(string pathForScreenshotFolder)
        {
            if (webDriver != null)
            {
                try
                {
                    Guid uuid = Guid.NewGuid();
                    string path = string.Format("{0}{1}.png", pathForScreenshotFolder, uuid);
                    Screenshot ss = ((ITakesScreenshot)webDriver).GetScreenshot();
                    ss.SaveAsFile(path, ScreenshotImageFormat.Png);
                    return path;
                }
                catch (Exception e)
                {
                    throw new Exception("Unable to take Screenshot: " + e.InnerException);
                }
            }

            return null;
        }

        #endregion

        #region Get Class And Method From Assembly

        private static Type GetClassTypeFromWebDriverAssembly(string classType)
        {
            classType.Should().NotBeNullOrEmpty();
            AssemblyName name = new AssemblyName("WebDriver");
            name.Should().NotBeNull();
            Assembly assembly = Assembly.Load(name);
            assembly.Should().NotBeNull();
            Type classTypeOptions = assembly.GetTypes().FirstOrDefault(item => item.Name.ToLower().Contains(classType.ToLower()));
            classTypeOptions.Should().NotBeNull();
            return classTypeOptions;
        }

        private static MethodInfo GetMethodFromClass(Type classType, string methodName)
        {
            MethodInfo method = classType.GetMethods().FirstOrDefault(item => item.Name.ToLower().Contains(methodName.ToLower()));
            method.Should().NotBeNull();
            return method;
        }

        #endregion
    }
}
