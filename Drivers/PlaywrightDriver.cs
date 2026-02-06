using Microsoft.Playwright;
using ReqnRollv3.Configs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnRollV3.Drivers
{

    public class PlaywrightDriver : IAsyncDisposable
    {
        //public IPlaywright Playwright { get; private set; }
        //public IBrowser Browser { get; private set; }
        //public IBrowserContext Context { get; private set; }
        //public IPage Page { get; private set; }

        // // Working 
        //public async Task InitializeAsync(string storageStatePath = null)
        //{
        //    Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        //    Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });

        //    var contextOptions = new BrowserNewContextOptions();
        //    if (!string.IsNullOrEmpty(storageStatePath) && File.Exists(storageStatePath))
        //        contextOptions.StorageStatePath = storageStatePath;

        //    Context = await Browser.NewContextAsync(contextOptions);
        //    Page = await Context.NewPageAsync();
        //    Page.SetDefaultTimeout(25_000);
        //    Page.SetDefaultNavigationTimeout(25_000);
        //}

        //private static readonly ConfigManager _configManager;
        public IPlaywright? Playwright { get; private set; }
        public IBrowser? Browser { get; private set; }
        public IBrowserContext? Context { get; private set; }
        public IPage? Page { get; private set; }

        //public PlaywrightDriver(ConfigManager configManager)
        //{
        //    _configManager = configManager;
        //}

        public async Task InitializeAsync(string? storageStatePath = null)
        {
            var browserOptions = ConfigManager.GetBrowserOptions();
            var contextOptions = ConfigManager.GetContextOptions();

            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                            {
                               Headless = browserOptions.Headless,
                               SlowMo = browserOptions.SlowMo,
                               Channel = browserOptions.Channel
                            });

            var newContextOptions = new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize
                {
                    Width = contextOptions.ViewportWidth,
                    Height = contextOptions.ViewportHeight
                },
                IgnoreHTTPSErrors = contextOptions.IgnoreHTTPSErrors,
                RecordVideoDir = Directory.GetCurrentDirectory().Split("bin")[0]


            };

            if (!string.IsNullOrEmpty(contextOptions.RecordVideoDir))
            {
                newContextOptions.RecordVideoDir = contextOptions.RecordVideoDir;
                //newContextOptions.RecordVideoSize = new VideoSize
                //{
                //    Width = contextOptions.RecordVideoSize.Width,
                //    Height = contextOptions.RecordVideoSize.Height
                //};
            }
            if (!string.IsNullOrEmpty(storageStatePath) && File.Exists(storageStatePath))
                newContextOptions.StorageStatePath = storageStatePath;

            Context = await Browser.NewContextAsync(newContextOptions);
            Page = await Context.NewPageAsync();
        }








        public async ValueTask DisposeAsync()
        {
            if (Page != null) await Page.CloseAsync();
            if (Context != null) await Context.CloseAsync();
            if (Browser != null) await Browser.CloseAsync();
            Playwright?.Dispose();
        }
    }

}
