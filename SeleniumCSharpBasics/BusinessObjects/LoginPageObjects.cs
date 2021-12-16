using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                double timeout = Convert.ToDouble(ConfigurationManager.AppSettings[Strings.TIMEOUT]);
                var SecureAnywhereURLKey = string.Format("SECUREANYWHEREURL_{0}",
                                        ConfigurationManager.AppSettings[Strings.ENVIRONMENT]);

                browser.LaunchBrowserAndNavigateToURLInput(ConfigurationManager.AppSettings[SecureAnywhereURLKey],
                                                            ConfigurationManager.AppSettings[Strings.BROWSER],
                                                            capabilities,
                                                            timeout);
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
                webrootLogo.GetElementExistsStatus().Should().BeTrue();
                loginTab.GetElementExistsStatus().Should().BeTrue();
                createAccountTab.GetElementExistsStatus().Should().BeTrue();
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
