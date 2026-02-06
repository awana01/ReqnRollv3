using System;
using System.Collections.Generic;

namespace ReqnRollv3.Configs
{
    public class TestSettings
    {
        public string? StateFolder { get; set; }
        public List<SiteConfig>? Sites { get; set; }
        public List<UserConfig>? Users { get; set; }
    }
}
