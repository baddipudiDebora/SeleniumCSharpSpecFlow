using BaseProject.PageElementLibrary;
using BaseProject.SeleniumHelpers;
using OpenQA.Selenium;

namespace SeleniumCSharpBasics.BusinessObjects
{
    class HotelsViewPageObjects
    {
        BrowserUtility browser = new BrowserUtility();
        Element firstHotel = new Element("xpath", "(//div[@class='HotelCardstyles__HotelInfoWrapperDiv-sc-1s80tyk-11 cmcTgu'])[1]");

        public string getFirstHotelText()
        {
            Synchronisation.WaitForPageToLoad();
          string firstHotelName =   firstHotel.GetElementTextValue();
          return firstHotelName;
          

        }
    }
}
