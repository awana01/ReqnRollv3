using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnRollV3.Support
{
    public static class LocatorExtensions
    {
        public static async Task<bool> Exists(this ILocator locator)
        {
            return await locator.CountAsync() > 0;
        }
    }

}
