using Microsoft.Playwright;
using NUnit.Framework;
using ReqnRollV3.Drivers;
using System.Globalization;

namespace ReqRollV2.StepDefinitions
{
    //[Binding]
    //public class StepDefinitions
    //{
    //    private readonly IPage _page;
    //    private readonly TestSettings _settings;

    //    // The driver is injected here; it's already initialized by the Hooks
    //    public StepDefinitions(PlaywrightDriver driver)
    //    {
    //        _page = driver.Page;
    //        _settings = ConfigurationReader.GetSettings();
    //    }

    //    [Given(@"I navigate to the application")]
    //    public async Task GivenINavigateToTheApplication()
    //    {
    //        // Use the BaseUrl from the config file
    //        await _page.GotoAsync(_settings.BaseUrl);
    //    }

    //    [Given(@"I am on the dashboard")]
    //    public async Task GivenIAmOnTheDashboard()
    //    {

    //        await _page.GotoAsync($"{_settings.BaseUrl}/dashboard");
    //    }

    //    [Then(@"I should see my username")]
    //    public async Task ThenIShouldSeeTheWelcomeMessage()
    //    {
    //        //// Locate the element and verify text
    //        //var welcomeText = await _page.Locator("#welcome-banner").InnerTextAsync();
    //        //Assert.That(welcomeText, Does.Contain(expectedMessage));
    //    }

    //    [Then(@"I should be on the login page")]
    //    public async Task ThenIShouldBeOnTheLoginPage()
    //    {
    //        //// Used for @Normal (unauthenticated) tests
    //        //var isVisible = await _page.Locator("text=Please Sign In").IsVisibleAsync();
    //        //Assert.That(isVisible, Is.True);
    //    }

    //    [When(@"I click on the ""(.*)"" button")]
    //    public async Task WhenIClickOnTheButton(string buttonText)
    //    {
    //        //await _page.ClickAsync($"button:has-text('{buttonText}')");
    //    }
    //}

    [Binding]
    public class OrangeHrmSteps
    {
        private readonly PlaywrightDriver _driver;
        private readonly IPage _page;

        public OrangeHrmSteps(PlaywrightDriver driver)
        {
            _driver = driver!;
            //_page = _driver.Page;
        }


        [Then(@"the dashboard should be visible")]
        public async Task ThenTheDashboardShouldBeVisible()
        {
            Thread.Sleep(10000);
            var header = _driver.Page!.Locator(".oxd-topbar-header-title");
            await Assertions.Expect(header).ToHaveTextAsync("Dashboard");
        }

        [Then(@"I view the employee list")]
        public async Task GivenIViewTheEmployeeList()
        {
            await _driver.Page!.ClickAsync("text=PIM");
            await Assertions.Expect(_driver.Page.Locator("span.oxd-topbar-header-breadcrumb")).ToHaveTextAsync("PIM");
        }

        [Then(@"user dropdown button should be visible")]
        public async Task f1()
        {
            Console.WriteLine("dashboard should visible");
            await Assertions.Expect(_driver.Page!.Locator("span.oxd-userdropdown-tab")).ToBeVisibleAsync();
            _driver.Page.Locator("\"text=PIM\"");
        }

        [When(@"click on (.*) menu")]
        public async Task f2(string menu_item) {

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string menuText = textInfo.ToTitleCase(menu_item);
            Console.WriteLine(menuText);
            Console.WriteLine("Hello World");

            switch (menuText)
            {
                case "Admin":
                    await _driver.Page!.ClickAsync($"text={menuText}");
                    break;
                case "PIM":
                    await _driver.Page!.ClickAsync($"text={menuText.ToUpper()}");
                    break;
                case "Leave":
                    await _driver.Page!.ClickAsync($"text={menuText}");
                    break;
                case "Time":
                    await _driver.Page!.ClickAsync($"text={menuText}");
                    break;
                default:
                    throw new NotImplementedException("No Menu Item is found");

            }



            await _driver.Page.ClickAsync("text=Admin");
            await Assertions.Expect(_driver.Page.Locator("span.oxd-topbar-header-breadcrumb")).ToContainTextAsync("Admin");
        }
        
        [Then(@"verify admin page")]
        public async Task f3() { 
        }

    }

}

