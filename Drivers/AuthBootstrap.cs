//using ReqnRollV3.Support;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ReqnRollV3.Drivers
//{
//    [Binding]
//    public class AuthBootstrap
//    {
//        [BeforeTestRun]
//        public static async Task Bootstrap()
//        {
//            var settings = ConfigurationReader.Get();
//            var driver = new PlaywrightDriver();
//            var auth = new AuthService(driver, settings.StateFolder);

//            foreach (var site in settings.Sites)
//                foreach (var user in settings.Users)
//                    await auth.EnsureAuth(user, site);
//        }
//    }

//}
