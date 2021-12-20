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
    class HotelsSearchPageObjects
    {
        BrowserUtility browser = new BrowserUtility();
        Element indiaRadioButton = new Element("xpath", "//h4[contains(text(),'India')]");
        TextBox placeSearchTextBox = new TextBox("xpath", "//input[@placeholder='e.g. - Area, Landmark or Hotel Name']");
        TextBox numberOfAdults = new TextBox("xpath", "//input[@value='2 Guests in 1 Room ']");
        Button deselectAdultsCount = new Button("xpath", "(//span[@class='PaxWidgetstyles__ContentActionIconWrapperSpan-sc-gv3w6r-8 dxKRvV'])[3]");
        Button doneButton = new Button("xpath", "//button[contains(text(),'Done')]");
        Button search = new Button("xpath", "//button[contains(text(),'Search Hotels')]");
        public void selectIndiaAndPlace()
        {
            indiaRadioButton.ClickElement();
            placeSearchTextBox.SetText("Ooty");
            Thread.Sleep(5000);
            placeSearchTextBox.SetText(Keys.ArrowDown);
            Thread.Sleep(5000);
            placeSearchTextBox.SetText(Keys.Enter);
            Thread.Sleep(5000);
            Console.WriteLine("succesfully entered Ooty");
        }

        public void selectNumberOfAdults()
        {
            numberOfAdults.ClickElement();
            deselectAdultsCount.ClickElement();
            doneButton.ClickElement();
        }
        public void clickOnSearch()
        {
            search.ClickElement();
        }
    }

}
