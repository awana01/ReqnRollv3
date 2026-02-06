using ReqnRollV3.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnRollv3.Steps
{
    using Reqnroll;
    using ReqnRollv3.Configs;

    [Binding]
    public class TestHooks
    {
        private readonly PlaywrightDriver _driver;
        private readonly AuthService _authService;

        public TestHooks(PlaywrightDriver driver, AuthService authService)
        {
            _driver = driver;
            _authService = authService;
        }

        [BeforeScenario]
        public async Task BeforeScenario(ScenarioContext scenarioContext)
        {
            
                var userTag = scenarioContext.ScenarioInfo.Tags.FirstOrDefault(t => t.StartsWith("User:"));
                var siteTag = scenarioContext.ScenarioInfo.Tags.FirstOrDefault(t => t.StartsWith("Site:"));
                
                var freshTag = scenarioContext.ScenarioInfo.Tags.FirstOrDefault(t => t.StartsWith("FreshLogin",StringComparison.OrdinalIgnoreCase))!;
                Console.WriteLine($"Scenario tag: {freshTag}");

            if (scenarioContext.ScenarioInfo.Tags.Contains("FreshTest"))
            {
                // Fresh context, no storage state, no login
                await _authService.PerformFreshLogin(_driver);
            }

            else
            {
                var userKey = userTag?.Split(':')[1] ?? "Admin";
                var siteKey = siteTag?.Split(':')[1] ?? "OrangeHRM";

                var user = ConfigManager.GetUser(userKey);
                var site = ConfigManager.GetSite(siteKey);
                await _authService.EnsureAuthenticatedContext(user, site);
            }


                //var userKey = userTag?.Split(':')[1] ?? "Admin";
                //var siteKey = siteTag?.Split(':')[1] ?? "OrangeHRM";

                //var user = ConfigManager.GetUser(userKey);
                //var site = ConfigManager.GetSite(siteKey);

                //await _authService.EnsureAuthenticatedContext(user, site);
            //}

        }

        

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _driver.DisposeAsync();
        }
    }


}
