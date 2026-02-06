using System;

namespace ReqnRollv3.Configs
{
    public class SiteConfig
    {
        public string? BaseUrl { get; set; }
        public string? Url { get; set; }
        public string GetBaseUrl() => BaseUrl!.TrimEnd('/');
        public SelectorConfig? Selectors { get; set; }
    }

    public class SelectorConfig
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? LoginButton { get; set; }
        public string? LoginSuccess { get; set; }
    }

}
