using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseProject.SeleniumHelpers
{
    public class BrowserUtility
    {

        public void LaunchBrowserAndNavigateToURLInput(string url, string browser, Dictionary<string, object> capablities, double timeout)
        {
            this.LaunchBrowserAndNavigateToURLInput(url, browser, capablities, timeout, ScreenSize.Maximized);
        }

        public void LaunchBrowserAndNavigateToURLInput(string url, string browser, Dictionary<string, object> capablities, double timeout, ScreenSize screenSize)
        {
            DriverUtility.SetupBrowser(browser, capablities, timeout, screenSize);
            DriverUtility.NavigateToURL(url);
        }

        public void LaunchBrowser(string browser, double timeout)
        {
            DriverUtility.SetupBrowser(browser, new Dictionary<string, object>(), timeout);
        }

        public void NavigateToUrl(string url)
        {
            DriverUtility.NavigateToURL(url);
        }

        public void CloseBrowser()
        {
            DriverUtility.CloseBrowser();
        }

        internal string GetBrowserTitle()
        {
            return DriverUtility.GetBrowserTitle();
        }

        internal By GetSeleniumLocatorIdentifier(string identifierType, string identifierValue)
        {
            return DriverUtility.GetElementByLocatorAndIdentifier(identifierType, identifierValue);
        }

        public void NavigateToRecentlyOpenedTab()
        {
            DriverUtility.webDriver.SwitchTo().Window(DriverUtility.webDriver.WindowHandles.Last());
        }

        public string NavigateToRecentlyOpenedTabAndGetURL()
        {
            DriverUtility.webDriver.SwitchTo().Window(DriverUtility.webDriver.WindowHandles.Last());
            return DriverUtility.webDriver.Url;
        }

        public void CloseRecentlyOpenedTabSwitchFocusToFirstTab()

        {

            DriverUtility.webDriver.Close();
            DriverUtility.webDriver.SwitchTo().Window(DriverUtility.webDriver.WindowHandles.First());

        }

        public void ClickAlertButton(string button)
        {
            IAlert alert = DriverUtility.webDriver.SwitchTo().Alert();

            if ("OK".Equals(button) || "Reload".Equals(button) || "Leave".Equals(button))
            {
                alert.Accept();
            }
            else if ("Cancel".Equals(button))
            {
                alert.Dismiss();
            }
            else
            {
                throw new ArgumentException($"Unsupported browser alert button: '{button}'.");
            }
        }

        public string GetAlertText()
        {
            // As of Chrome 93.0.4577.63 getting the alert text via Selenium is not working (an empty string is returned).
            return DriverUtility.webDriver.SwitchTo().Alert().Text;
        }

        public bool IsAlertDisplayed()
        {
            try
            {
                //  As of Firefox 91.0.1 the alerts are NOT being displayed at all when the test is run via automation. They are displayed when run manually.
                //  It is possibly caused by GeckoDriver changing a Firefox configuration setting, but I haven't been able to figure out which one.
                DriverUtility.webDriver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        public void CloseBrowserTab()
        {
            DriverUtility.CloseBrowserTab();
        }

        public void SwitchToTab(string url)
        {
            DriverUtility.SwitchToTab(url);
        }

        public string GetBrowserURL()
        {
            return DriverUtility.GetBrowserURL();
        }

        public void NavigateBackOnBrowser()
        {
            DriverUtility.NavigateBackOnBrowser();
        }

        public void NavigateForwardOnBrowser()
        {
            DriverUtility.NavigateForwardOnBrowser();
        }

    }

    [TestFixture]
    public class TestBrowserUtility
    {
        BrowserUtility browserHelper;
        Dictionary<string, object> capablitiesStructure = new Dictionary<string, object>();

        [SetUp]
        public void Setup()
        {
            browserHelper = new BrowserUtility();
        }

        [Test]
        public void NavigateToURLOnChromeBrowserAndValidateTitle()
        {
            browserHelper.LaunchBrowserAndNavigateToURLInput("http://www.google.com", "Chrome", capablitiesStructure, 5);
            browserHelper.GetBrowserTitle().Should().Be("Google");
        }

        [Test]
        public void NavigateToURLOnInternetExplorerBrowserAndValidateTitle()
        {
            browserHelper.LaunchBrowserAndNavigateToURLInput("http://www.google.com", "IE", capablitiesStructure, 5);
            browserHelper.GetBrowserTitle().Should().Be("Google");
        }

        [Test]
        public void NavigateToURLOnFirefoxBrowserAndValidateTitle()
        {
            browserHelper.LaunchBrowserAndNavigateToURLInput("http://www.google.com", "Firefox", capablitiesStructure, 5);
            browserHelper.GetBrowserTitle().Should().Be("Google");
        }

        [Test]
        public void LaunchChromeBrowserAndNavigateToURLAndValidateTitle()
        {
            browserHelper.LaunchBrowser("Chrome", 5);
            browserHelper.NavigateToUrl("http://www.google.com");
            browserHelper.GetBrowserTitle().Should().Be("Google");
        }

        [Test]
        public void LaunchIEBrowserAndNavigateToURLAndValidateTitle()
        {
            browserHelper.LaunchBrowser("IE", 5);
            browserHelper.NavigateToUrl("http://www.google.com");
            browserHelper.GetBrowserTitle().Should().Be("Google");
        }

        [Test]
        public void LaunchFireFoxBrowserAndNavigateToURLAndValidateTitle()
        {
            browserHelper.LaunchBrowser("Firefox", 5);
            browserHelper.NavigateToUrl("http://www.google.com");
            browserHelper.GetBrowserTitle().Should().Be("Google");
        }

        [Test]
        public void GetElementFromPageUsingIdAndValidateByIdObjectIsReturned()
        {
            By objById = browserHelper.GetSeleniumLocatorIdentifier("Id", "q");
            objById.ToString().Should().Be("By.Id: q");
        }

        [Test]
        public void GetElementFromPageUsingClassNameAndValidateByClassNameObjectIsReturned()
        {
            By objById = browserHelper.GetSeleniumLocatorIdentifier("classname", "q");
            objById.ToString().Should().Be("By.ClassName[Contains]: q");
        }

        [Test]
        public void GetElementFromPageUsingCssSelectorAndValidateByCssSelectorObjectIsReturned()
        {
            By objById = browserHelper.GetSeleniumLocatorIdentifier("cssSelector", "q");
            objById.ToString().Should().Be("By.CssSelector: q");
        }

        [Test]
        public void GetElementFromPageUsingLinkTextAndValidateByLinkTextObjectIsReturned()
        {
            By objById = browserHelper.GetSeleniumLocatorIdentifier("LINKTEXT", "q");
            objById.ToString().Should().Be("By.LinkText: q");
        }

        [Test]
        public void GetElementFromPageUsingPartialLinkTextAndValidateByPartialLinkTextObjectIsReturned()
        {
            By objById = browserHelper.GetSeleniumLocatorIdentifier("PartialLinkText", "q");
            objById.ToString().Should().Be("By.PartialLinkText: q");
        }

        [Test]
        public void GetElementFromPageUsingTagNameAndValidateByTagNameObjectIsReturned()
        {
            By objById = browserHelper.GetSeleniumLocatorIdentifier("tagname", "q");
            objById.ToString().Should().Be("By.TagName: q");
        }

        [Test]
        public void GetElementFromPageUsingXPathAndValidateByXPathObjectIsReturned()
        {
            By objById = browserHelper.GetSeleniumLocatorIdentifier("xpath", "q");
            objById.ToString().Should().Be("By.XPath: q");
        }

        [Test]
        public void GetElementFromPageUsingNameAndValidateByNameObjectIsReturned()
        {
            By objById = browserHelper.GetSeleniumLocatorIdentifier("name", "q");
            objById.ToString().Should().Be("By.Name: q");
        }




        [TearDown]
        public void TearDown()
        {
            if (DriverUtility.webDriver != null)
            {
                browserHelper.CloseBrowser();
            }

        }
    }
}
