using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharpBasics.StepDefinitions
{
    [Binding]
    class CommonStepDefinitions
    {
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

