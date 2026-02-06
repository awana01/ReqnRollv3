using Microsoft.Playwright;
using ReqnRollv3.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace ReqnRollV3.Drivers
{
    public class AuthService
    {
        private readonly PlaywrightDriver _driver;
        private readonly string _stateFolder;

        public AuthService(PlaywrightDriver driver)
        {
            _driver = driver;
            _stateFolder = Path.Combine(Directory.GetCurrentDirectory(), ConfigManager.GetStateFolder());
            if (!Directory.Exists(_stateFolder)) Directory.CreateDirectory(_stateFolder);
        }


        public async Task<string> EnsureAuthenticatedContext(
    UserConfig user,
    SiteConfig site)
        {
            string fullStatePath = Path.Combine(_stateFolder, user.FileName);

            await _driver.InitializeAsync(fullStatePath);

            Console.WriteLine($"Before browser navig: {DateTime.Now:hh:mm:ss tt}");

            await _driver.Page!.GotoAsync(site.GetBaseUrl() + site.Url);

            Console.WriteLine($"After browser navig: {DateTime.Now:hh:mm:ss tt}");

            // 1️⃣ Check if already logged in
            if (await ExistsAsync(_driver, site.Selectors.LoginSuccess, 15000))
            {
                Console.WriteLine("Already logged in using stored auth state");
                return fullStatePath;
            }

            // 2️⃣ Not logged in → wait for login UI
            bool loginUiReady = await WaitUntilInteractableAsync(_driver,site.Selectors.Username, 10000);

            if (!loginUiReady)
                throw new Exception("Login page did not load as expected");

            // 3️⃣ Decide login behavior
            if (!File.Exists(fullStatePath))
            {
                await LoginFunc1(_driver, site, user, fullStatePath);
            }
            else
            {
                Console.WriteLine("Stored auth invalid, re-authenticating");
                await LoginFunc1(_driver, site, user, fullStatePath);
            }

            return fullStatePath;
        }











        // // Working code:
        //public async Task<string> EnsureAuthenticatedContext(UserConfig user, SiteConfig site)
        //{
        //    string fullStatePath = Path.Combine(_stateFolder, user.FileName);

        //    await _driver.InitializeAsync(fullStatePath);

        //    var  timeOnly1 = DateTime.Now.ToString("hh:mm:ss tt");
        //    Console.WriteLine($"Before browser navig: {timeOnly1}");

        //    await _driver.Page!.GotoAsync(site.GetBaseUrl() + site.Url); 

        //    var timeOnly2 = DateTime.Now.ToString("hh:mm:ss tt");
        //    Console.WriteLine($"After browser navig: {timeOnly2}");


        //    if (await _driver.Page.Locator(site.Selectors.LoginSuccess).IsVisibleAsync())
        //    {
        //        Console.WriteLine("Already Logged In to App");
        //    }
        //    else
        //    {
        //        if (await _driver.Page.Locator(site.Selectors.Username).IsVisibleAsync() && !File.Exists(fullStatePath))
        //        { await LoginFunc1(_driver, site, user, fullStatePath); }

        //        //if (isLoginPage && File.Exists(fullStatePath))
        //        if (await _driver.Page.Locator(site.Selectors.Username).IsVisibleAsync() && File.Exists(fullStatePath))
        //        {
        //            var timeOnly3 = DateTime.Now.ToString("hh:mm:ss tt");
        //            Console.WriteLine($"try new login: {timeOnly3}");
        //            await LoginFunc1(_driver, site, user, fullStatePath); 
        //        }
        //    }
        //    return fullStatePath;
        //}

        public async Task PerformFreshLogin(PlaywrightDriver driver)
        {
           await driver.InitializeAsync();
           
        }

        private async Task LoginFunc1(PlaywrightDriver _driver,SiteConfig site,UserConfig user, string AuthFullStatePath)
        {
            await _driver.Page!.FillAsync(site.Selectors.Username, user.Username);
            await _driver.Page.FillAsync(site.Selectors.Password, user.Password);
            await _driver.Page.ClickAsync(site.Selectors.LoginButton);
            await _driver.Page.WaitForSelectorAsync(site.Selectors.LoginSuccess, new() { Timeout = 35000 });
            await _driver.Context!.StorageStateAsync(new() { Path = AuthFullStatePath });
        }


        public static async Task WaitForPageReadyAsync(PlaywrightDriver driver, int timeoutMs = 30000)
        {
            await WaitForDomReadyAsync(driver, timeoutMs);
            //await InjectNetworkTrackerAsync(driver);
            //await WaitForNetworkIdleAsync(driver, timeoutMs);
            //await WaitForUiReadyAsync(driver);
        }





        public static async Task WaitForDomReadyAsync(PlaywrightDriver driver, int timeoutMs = 30000)
        {
            await driver.Page!.WaitForFunctionAsync(
                                "() => document.readyState === 'complete'",
                                new PageWaitForFunctionOptions { Timeout = timeoutMs }
            );
        }

        public static async Task InjectNetworkTrackerAsync(PlaywrightDriver driver)
        {
            await driver.Page!.EvaluateAsync(@" (() => {
                if (window.__pendingRequests !== undefined) return;
                   
                window.__pendingRequests = 0;

                const open = XMLHttpRequest.prototype.open;
                XMLHttpRequest.prototype.open = function () {
                   this.addEventListener('loadstart', () => window.__pendingRequests++);
                   this.addEventListener('loadend', () => window.__pendingRequests--);
                   open.apply(this, arguments);
               };

               const originalFetch = window.fetch;
               window.fetch = function () {
                  window.__pendingRequests++;
                  return originalFetch.apply(this, arguments).finally(() => window.__pendingRequests--);
               };
            })();
        ");
        }



        public static async Task WaitForNetworkIdleAsync(PlaywrightDriver driver, int timeoutMs = 30000)
        {
            await driver.Page!.WaitForFunctionAsync(
                "() => window.__pendingRequests === 0",
                new PageWaitForFunctionOptions() { Timeout=timeoutMs}
            );
        }

        public static async Task WaitForUiReadyAsync(PlaywrightDriver driver)
        {
            bool boolVal = true;
            await driver.Page.WaitForFunctionAsync(@"
                       () => {
                          const loaders = document.querySelectorAll('.loader, .spinner, [aria-busy=true]'
                    );
            return loaders.length === 0;
            }");
        }


        public static async Task WaitForPageReady1Async(PlaywrightDriver driver,int timeoutMs = 30000)
        {
            var page = driver.Page!;

            Console.WriteLine("⏳ Waiting for DOM ready...");
            await page.WaitForFunctionAsync(
                "() => document.readyState === 'complete'",
                new PageWaitForFunctionOptions { Timeout = timeoutMs }
            );
            Console.WriteLine("✅ DOM ready");

            Console.WriteLine("⏳ Injecting network tracker...");
            await InjectNetworkTrackerAsync(driver);

            Console.WriteLine("⏳ Waiting for network idle...");
            await page.WaitForFunctionAsync(
                "() => window.__pendingRequests === 0",
                new PageWaitForFunctionOptions { Timeout = timeoutMs }
            );
            Console.WriteLine("✅ Network idle");

            Console.WriteLine("⏳ Waiting for UI ready...");
            await page.WaitForFunctionAsync(
                "() => !document.querySelector('.loader,.spinner,[aria-busy=\"true\"]')",
                new PageWaitForFunctionOptions { Timeout = timeoutMs }
            );
            Console.WriteLine("✅ UI ready");
        }



        public static async Task<bool> ExistsAsync(PlaywrightDriver driver, string selector, int timeoutMs = 3000)
        {
            try
            {
                await driver.Page!.Locator(selector).WaitForAsync(new()
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = timeoutMs
                });
                return true;
            }
            catch
            {
                return false;
            }
        }





        public static async Task<bool> WaitUntilInteractableAsync(PlaywrightDriver driver, string selector,int timeoutMs = 30000)
        {
           if (driver.Page == null)
               throw new ArgumentNullException(nameof(driver.Page));
           try
           {
            // 1️⃣ Wait for element to appear in DOM & be visible
            await driver.Page.Locator(selector).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = timeoutMs
            });

            // 2️⃣ Wait until element is truly interactable
            await driver.Page.WaitForFunctionAsync(
                @"selector => {
                    const el = document.querySelector(selector);
                    if (!el) return false;

                    const style = window.getComputedStyle(el);
                    const rect = el.getBoundingClientRect();

                    return rect.width > 0 &&
                       rect.height > 0 &&
                       style.visibility !== 'hidden' &&
                       style.display !== 'none' &&
                       !el.disabled;
                }",
                selector,
                new PageWaitForFunctionOptions
                {
                    Timeout = timeoutMs,
                    PollingInterval = 1000
                });

            return true;
        }
        catch (PlaywrightException)
        {
            return false;
        }
    }
















}
}



//bool isLoginPage = await _driver.Page.Locator(site.Selectors.Username).IsEnabledAsync();
//if (isLoginPage && !File.Exists(fullStatePath))
//await _driver.Page.FillAsync(site.Selectors.Username, user.Username);
//await _driver.Page.FillAsync(site.Selectors.Password, user.Password);
//await _driver.Page.ClickAsync(site.Selectors.LoginButton);
//await _driver.Page.WaitForSelectorAsync(site.Selectors.LoginSuccess, new() { Timeout = 15000 });
//await _driver.Context.StorageStateAsync(new() { Path = fullStatePath });