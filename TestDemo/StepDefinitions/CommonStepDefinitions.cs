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
        private readonly ScenarioContext context;

        public CommonStepDefinitions(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

       

        [Given(@"I launch the Browser and navigate to Webpage")]
        public void GivenILaunchTheBrowserAndNavigateToWebrootSecureAnywhereWebpage()
        {
            loginPage.LaunchWebrootSecureAnywhereWebsite();
        }

        [Then(@"I verify the '(.*)' Page is displayed")]
        public void ThenIVerifyThePageOnTheWebsiteIsDisplayed(string pageName)
        {
            commonObjects.VerifyPageIsDisplayed(pageName);
        }

    }
}

