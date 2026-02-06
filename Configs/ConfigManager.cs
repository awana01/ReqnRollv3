using Microsoft.Extensions.Configuration;

namespace ReqnRollv3.Configs
{
    public static class ConfigManager
    {
        private static IConfigurationRoot _config;

        static ConfigManager()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static UserConfig GetUser(string key)
        {
            return _config.GetSection($"Users:{key}").Get<UserConfig>()!;
        }

        public static SiteConfig GetSite(string key)
        {
            return _config.GetSection($"Sites:{key}").Get<SiteConfig>()!;
        }

        public static string GetStateFolder()
        {
            return _config.GetSection("Settings:StateFolder").Value!;
        }

        public static BrowserOptions GetBrowserOptions()
        {
            var options = _config.GetSection("BrowserOptions").Get<BrowserOptions>() ?? new BrowserOptions();
            return options;
        }

        public static ContextOptions GetContextOptions()
        {
            var options = _config.GetSection("ContextOptions").Get<ContextOptions>() ?? new ContextOptions();
            return options;
        }
    }
}
