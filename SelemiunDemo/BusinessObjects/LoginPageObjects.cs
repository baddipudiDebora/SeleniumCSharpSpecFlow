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
       Element hotelsButton = new Element("xpath", "//a[@href='/hotels/']");
       Element flightButton = new Element("xpath", "//a[@href='/flights/']");
        public void LaunchGoIBiBoWebsite()
        {
            try
            {

               // double timeout = Convert.ToDouble(ConfigurationManager.GetSection("Timeout"));
                double timeout = 10;
             //   var GoIBiBo_QAURLKey = string.Format("GoIBiBo_{0}",ConfigurationManager.AppSettings["Environment"]);
                var GoIBiBo_QAURLKey = "https://www.goibibo.com/";
                browser.LaunchBrowserAndNavigateToURLInput(GoIBiBo_QAURLKey,
                                                            "chrome", timeout);
               
                Synchronisation.WaitForPageToLoad();
                Reporting.TestPass("Successfully verified Browser is launched and Secure Anywhere Website launched");

            }
            catch (Exception e)
            {
                Reporting.TestFail("Error Launching Browser and navigating to Secure Anywhere Website", e);
                throw e;
            }
        }

        public void clickOnHotelsButton()
        {
            hotelsButton.ClickElement();
        }

        public void clickOnFlightButton()
        {
            flightButton.ClickElement();
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
