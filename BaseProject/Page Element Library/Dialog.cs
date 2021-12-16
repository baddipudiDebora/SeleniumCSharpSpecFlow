using System;
using BaseProject.SeleniumHelpers;
using FluentAssertions;
using OpenQA.Selenium.Support.UI;

namespace BaseProject.PageElementLibrary
{
    public class Dialog 
    {
       public static void WaitUntilDialogDisplays(Element dialog)
        {
            try
            {
                var byLocator = dialog.GetElementByLocator();
                var wait = new WebDriverWait(DriverUtility.webDriver, TimeSpan.FromSeconds(60));
                wait.Until(ExpectedConditions.ElementIsVisible(byLocator));
            }
            catch (Exception e)
            {
                throw new Exception("Error waiting for Dialog to appear", e.InnerException);
            }
        }

        public static void WaitUntilDialogIsClosed(Element dialog)
        {
            try
            {
                var byLocator = dialog.GetElementByLocator();
                var wait = new WebDriverWait(DriverUtility.webDriver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(byLocator));
            }
            catch (Exception e)
            {
                throw new Exception("Error waiting for Dialog to close", e.InnerException);
            }
        }
         
        public static void VerifyDialogText(Element dialog, string dialogText)
        {
            try
            {
                var displayedText = dialog.GetElementAttributeValue("innerText");
                displayedText.ToLower().Should().Contain(dialogText.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("Error validating Dialog text", e.InnerException);
            }
        }

        public static void VerifyDialogTextAndClickOnDialogButton(Element dialog, Button btnToClick, string dialogText)
        { 
            try
            {
                VerifyDialogText(dialog, dialogText);
                btnToClick.ClickElement();
            }
            catch (Exception e)
            {
                throw new Exception("Error validating Dialog text and click button", e.InnerException);
            }
        }

    }
}