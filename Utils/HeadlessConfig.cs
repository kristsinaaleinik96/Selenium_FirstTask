using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_FirstTask.Utils
{
    internal class HeadlessConfig
    {
        public static ChromeOptions GetChromeOptions(bool isHeadless)
        {
            var options = new ChromeOptions();
            if (isHeadless)
            {
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--window-size=1920,1080");
            }
            return options;
        }

        public static FirefoxOptions GetFirefoxOptions(bool isHeadless)
        {
            var options = new FirefoxOptions();
            if (isHeadless)
            {
                options.AddArgument("--headless");
            }
            return options;
        }

        public static EdgeOptions GetEdgeOptions(bool isHeadless)
        {
            var options = new EdgeOptions();
            if (isHeadless)
            {
                options.AddArgument("headless");
            }
            return options;
        }
    }
}