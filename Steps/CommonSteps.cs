using Microsoft.Playwright;
using ReqnRollV3.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnRollv3.Steps
{
    [Binding]
    public class CommonSteps
    {

        public readonly PlaywrightDriver _driver;
        public readonly IPage _page;

        public CommonSteps(PlaywrightDriver driver)
        {
            _page = driver.Page;
        }


        [Given(@"I navigate to {string}")]
        [Given(@"user navigates to {string}")]
        [Given(@"I opened the {string}")]
        public async Task navig1(string url)
        {
            await _page.GotoAsync(url, new PageGotoOptions() { Timeout = 25_000 });
        }




    }
}
