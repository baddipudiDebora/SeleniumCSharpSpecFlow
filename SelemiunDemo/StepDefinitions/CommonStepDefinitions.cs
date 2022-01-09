using BaseProject.BusinessObjects;
using SeleniumCSharpBasics.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumCSharpBasics.StepDefinitions
{
    [Binding]
    class CommonStepDefinitions
    {
        private CommonPageObjects commonObjects = new CommonPageObjects();
        private LoginPageObjects loginPage = new LoginPageObjects();
        private HotelsSearchPageObjects searchHotelsPage = new HotelsSearchPageObjects();
       private HotelsViewPageObjects hotelsViewPageObjects = new HotelsViewPageObjects();
        private readonly ScenarioContext context;

        public CommonStepDefinitions(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [Given(@"I launch the Browser and navigate to Webpage")]
        public void GivenILaunchTheBrowserAndNavigateToWebrootSecureAnywhereWebpage()
        {
            loginPage.LaunchGoIBiBoWebsite();
        }
        [When(@"I verify the '(.*)' Page is displayed")]
        public void ThenIVerifyThePageOnTheWebsiteIsDisplayed(string pageName)
        {
            commonObjects.VerifyPageIsDisplayed(pageName);
        }


        [Then(@"I click on '([^']*)' Button")]
        public void ThenIClickOnButton(string buttonName)
        { if (buttonName == "Hotels")
            { 
                loginPage.clickOnHotelsButton();
            }
        else if (buttonName == "Flights")
            {
                loginPage.clickOnFlightButton();
                    }
        }
        [Given(@"I select India and enter City name")]
        public void GivenISelectIndiaAndEnterCityName()
        {
            searchHotelsPage.selectIndiaAndPlace();
        }

        [When(@"I Select one adult under Rooms option")]
        public void WhenSelectOneAdultUnderRoomsOption()
        {
            searchHotelsPage.selectNumberOfAdults();
        }
        [When(@"Click on the “Get Set Go” button")]
        public void WhenClickOnTheGetSetGoButton()
        {
            searchHotelsPage.clickOnSearch();
        }

        [Then(@"I Log hotel name and hotel search count")]
        public void ThenILogHotelNameAndHotelSearchCount()
        {
          hotelsViewPageObjects.getFirstHotelText();
        }
    }
}

