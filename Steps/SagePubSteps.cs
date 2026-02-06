using Microsoft.Playwright;
using Reqnroll;
using ReqnRollV3.Drivers;

namespace ReqnRollv3.Steps
{
    [Binding]
    public sealed class SagePubSteps
    {
        public readonly PlaywrightDriver _driver;
        public readonly IPage _page;
        public SagePubSteps(PlaywrightDriver driver) 
        {
            _driver = driver;
            _page= _driver.Page!;
        }

        [When(@"user logged in with credentials {string} and {string}")]
        public async Task Sp1(string email,string pwd)
        {
            ILocator acceptBTN = _page.Locator("button#onetrust-accept-btn-handler");
            //_page.Dialog += async (_, dialog) =>
            //{
            //    Console.WriteLine($"Closing Cookies Dialog: {dialog.Message}");
            //    await acceptBTN.ClickAsync();
            //};


            //_page.Load += async (_, _) =>
            //{
            //    var btn = _page.Locator("button#onetrust-accept-btn-handler");
            //    if (await btn.IsVisibleAsync())
            //        await btn.ClickAsync();
            //};

            await _page.AddLocatorHandlerAsync(acceptBTN, async (p) =>
            {
                Console.WriteLine("Banner cookies model is opened");
                await p.ClickAsync();
                Console.WriteLine("Banner cookies model is closed");
            });



            await _page.Locator("button#login-form").ClickAsync();
            await _page.GetByLabel("Email address:").ClickAsync();
            await _page.GetByLabel("Email address:").FillAsync(email);

            await _page.GetByLabel("Password:").ClickAsync();
            await _page.GetByLabel("Password:").FillAsync(pwd);

            await _page.Locator("text=Remember me").ClickAsync();
            Thread.Sleep(2000);
            //await _page.Locator("#login-modal").GetByRole(AriaRole.Button, new() { Name = "Sign In", Exact = true }).ClickAsync(new() { ClickCount=1});

            await _page.Locator("form[action='/sitefinity/crm-login-handler'] button[type='submit']").ClickAsync();
            
            //Thread.Sleep(2000);
            //await _page.Locator("#login-modal").GetByRole(AriaRole.Button, new() { Name = "Sign In", Exact = true }).ClickAsync();
            //await _page.Locator("button[data-sf-role='sf-login-button']").Nth(0).ClickAsync();
            await _page.WaitForSelectorAsync("button#login-details", new() { Timeout = 25_000 });



            //_page.Popup += async (_, acceptBTN) =>
            //{
            //    Console.WriteLine("Popup detected. Closing...");
            //    await acceptBTN.ClickAsync();
            //};

            //await _page.WaitForPopupAsync(async (acceptBTN) =>
            //{
            //    acceptBTN;
            //});




        }
        
        [Then(@"user sucessfully logged in")]
        public async Task Sp2()
        {
            await _page.WaitForSelectorAsync("button#login-details", new() { Timeout = 25_000 });
        }





    }
}