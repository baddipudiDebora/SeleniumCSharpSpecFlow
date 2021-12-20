using BaseProject.PageElementLibrary;
using BaseProject.SeleniumHelpers;
using OpenQA.Selenium;

namespace SeleniumCSharpBasics.BusinessObjects
{
    class HotelsViewPageObjects
    {
        BrowserUtility browser = new BrowserUtility();
        Element firstHotel = new Element("xpath", "(//h4[@class='dwebCommonstyles__SmallSectionHeader-sc-112ty3f-7 hAEfdZ'])[1]");

        public string getFirstHotelText()
        {
            Synchronisation.WaitForPageToLoad();
          string firstHotelName =   firstHotel.GetElementTextValue();
          return firstHotelName;
          

        }
    }
}
