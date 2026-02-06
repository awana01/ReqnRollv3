using Microsoft.Playwright;
using ReqnRollV3.Drivers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnRollv3.Steps
{
    [Binding]
    public class DemoQASteps
    {
        public readonly PlaywrightDriver _driver;
        public DemoQASteps(PlaywrightDriver driver)
        {
            _driver = driver!;
        }

        [Then(@"user loogged in sucessfully")]
        public async Task Sp1()
        {
           string? userMail= await _driver.Page!.Locator("div[class='header-links']>ul>li:nth-child(1)").TextContentAsync();
            Console.WriteLine(userMail);
           await Assertions.Expect(_driver.Page.Locator("div[class='header-links']>ul>li:nth-child(1)")).ToContainTextAsync("zora123@yopmail.com");
        }

        [When(@"clicks on {string}")]
        public void WhenClicksOn(string menu_item)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string menuItems = textInfo.ToTitleCase(menu_item);
            Console.WriteLine(menuItems);
            
            switch (menuItems) 
            {
                case "Books":
                    _driver.Page!.Locator($"ul[class='top-menu']>li>a:has-text('{menuItems}')").ClickAsync();
                    break;
                case "Computers":
                    _driver.Page!.Locator($"ul[class='top-menu']>li>a:has-text('{menuItems}')").ClickAsync();
                    break;
                case "Apparel & Shoes":
                    
                    _driver.Page!.Locator($"ul[class='top-menu']>li>a:has-text('{menuItems}')").ClickAsync();
                    break;
                default:
                    throw new Exception("Option is not defined");
                
            }
            Thread.Sleep(5000);

        }

        [Then(@"verify page title as {string}")]
        public async Task Sp2(string pageTitle) {

            await Assertions.Expect(_driver.Page!.Locator("div[class='page-title']>h1")).ToContainTextAsync(pageTitle);
        }






        [When(@"some step")]
        public async Task f1()
        {
            //await _driver.Page.GotoAsync("https://demowebshop.tricentis.com/");
            //await _driver.Page.GetByRole(AriaRole.Link, new() { Name = "Log in" }).ClickAsync();
            //await _driver.Page.GetByRole(AriaRole.Textbox, new() { Name = "Email:" }).ClickAsync();
            //await _driver.Page.GetByRole(AriaRole.Link, new() { Name = "Register" }).ClickAsync();
            //await _driver.Page.GetByRole(AriaRole.Radio, new() { Name = "Male", Exact = true }).CheckAsync();
            //await _driver.Page.GetByRole(AriaRole.Textbox, new() { Name = "First name:" }).ClickAsync();
            //await _driver.Page.GetByRole(AriaRole.Textbox, new() { Name = "First name:" }).FillAsync("Zora");
            ////await Page.GetByRole(AriaRole.Textbox, new() { Name = "Last name:" }).ClickAsync();
            ////await Page.GetByRole(AriaRole.Textbox, new() { Name = "Last name:" }).FillAsync("Var");
            ////await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email:" }).ClickAsync();
            ////await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email:" }).FillAsync("zora123@yopmail.com");
            ////await Page.GetByRole(AriaRole.Textbox, new() { Name = "Password:", Exact = true }).ClickAsync();
            ////await Page.GetByRole(AriaRole.Textbox, new() { Name = "Password:", Exact = true }).FillAsync("test123");
            ////await Page.GetByRole(AriaRole.Textbox, new() { Name = "Confirm password:" }).ClickAsync();
            ////await Page.GetByRole(AriaRole.Textbox, new() { Name = "Confirm password:" }).FillAsync("test123");
            ////await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
            ////await Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            ////await Page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "Books" }).Nth(2).ClickAsync();
            ////await Page.GetByRole(AriaRole.Link, new() { Name = "Books" }).Nth(1).ClickAsync();
            //await _driver.Page.GetByRole(AriaRole.Link, new() { Name = "Picture of Computing and" }).ClickAsync();
            //await _driver.Page.Locator("#add-to-cart-button-13").ClickAsync();
            //await _driver.Page.Locator("#bar-notification").ClickAsync();
            //await _driver.Page.GetByRole(AriaRole.Link, new() { Name = "Shopping cart (1)" }).ClickAsync();
            //await _driver.Page.GetByRole(AriaRole.Button, new() { Name = "Checkout" }).ClickAsync();
            //await _driver.Page.GetByRole(AriaRole.Button, new() { Name = "close" }).ClickAsync();
            //await _driver.Page.GetByRole(AriaRole.Link, new() { Name = "Log out" }).ClickAsync();
            //await _driver.Page.GetByRole(AriaRole.Link, new() { Name = "Log in" }).ClickAsync();

            //await _driver.Page.GetByRole(AriaRole.Textbox, new() { Name = "Email:" }).ClickAsync();
            //await _driver.Page.GetByRole(AriaRole.Textbox, new() { Name = "Email:" }).FillAsync("zora123@yopmail.com");
            //await _driver.Page.GetByRole(AriaRole.Textbox, new() { Name = "Password:" }).ClickAsync();
            //await _driver.Page.GetByRole(AriaRole.Textbox, new() { Name = "Password:" }).FillAsync("test123");
            //await _driver.Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        }
    }
}
