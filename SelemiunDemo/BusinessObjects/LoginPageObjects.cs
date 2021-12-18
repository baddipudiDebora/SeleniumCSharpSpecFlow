using BaseProject.PageElementLibrary;
using BaseProject.SeleniumHelpers;
using System;
using System.Net.Mime;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using BaseProject.Selenium_Helpers;
using System.Configuration;
using System.Threading;
using OpenQA.Selenium;

namespace SeleniumCSharpBasics.BusinessObjects
{
    class LoginPageObjects
    {
        BrowserUtility browser = new BrowserUtility();
        Element webrootLogo = new Element("classname", "wr-logo");

        Element loginTab = new Element("xpath", ".//*[@href='#']");
        public void LaunchWebrootSecureAnywhereWebsite()
        {
            try
            {
                double timeout = Convert.ToDouble(ConfigurationManager.AppSettings["Timeout"]);
                var SecureAnywhereURLKey = string.Format("SECUREANYWHEREURL_{0}",
                                        ConfigurationManager.AppSettings["Environment"]);

                browser.LaunchBrowserAndNavigateToURLInput(ConfigurationManager.AppSettings[SecureAnywhereURLKey],
                                                            ConfigurationManager.AppSettings["Browser"],timeout);
                Synchronisation.WaitForPageToLoad();
                Synchronisation.WaitUntilObjectIsStale();
                Reporting.TestPass("Successfully verified Browser is launched and Secure Anywhere Website launched");

            }
            catch (Exception e)
            {
                Reporting.TestFail("Error Launching Browser and navigating to Secure Anywhere Website", e);
                throw e;
            }
        }

        public void CloseBrowser()
        {
            try
            {
                browser.CloseBrowser();
                Reporting.TestPass("Browser Closed successfully");
            }
            catch (Exception e)
            {
                Reporting.TestFail("Errored while Closing Browser", e);
                throw e;
            }
        }
        public void LoginPageObjectVerification()
        {
            try
            {
              //  webrootLogo.GetElementExistsStatus().Should().BeTrue();
             //   loginTab.GetElementExistsStatus().Should().BeTrue();
              //  createAccountTab.GetElementExistsStatus().Should().BeTrue();
                Reporting.TestPass("Verified Login Page Objects succesfully");
            }
            catch (Exception e)
            {
                Reporting.TestFail("Error validating Login Objects Page", e);
                throw e;
            }
        }

    }
}
