using BaseProject.PageElementLibrary;
using BaseProject.SeleniumHelpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProject.BusinessObjects
{
    public class CommonPageObjects
    {
        private BrowserUtility browser = new BrowserUtility();


        private Element loggedInUser = new Element("cssSelector", "[data-testid='user']");

    
        public void VerifyPageIsDisplayed(string pageName)
        {
            RunPageVerificationMethodForInputPage(pageName);
        }

     
    
        public void RunPageVerificationMethodForInputPage(string pageName)
        {
            DriverUtility.GetBrowserTitle().Should().BeEquivalentTo(pageName);
        }

   
     
        internal void RefreshBrowser()
        {
            DriverUtility.RefreshBrowser();
            Synchronisation.WaitForPageToLoad();
            Synchronisation.WaitUntilObjectIsStale();
        }

     

     
        public string GetLoggedInUser()
        {
            var userName = loggedInUser.GetElementTextValue();
            return userName;
        }

        public void WaitForTime(int time)
        {
            time = time * 1000;
            Thread.Sleep(time);
        }
    }
}
