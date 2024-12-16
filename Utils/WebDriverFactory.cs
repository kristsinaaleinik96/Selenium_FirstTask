using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium_FirstTask.Utils
{
    internal class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(string browser, bool isHeadless)
        {
            switch (browser.ToLower())
            {
                case "chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    var chromeOptions = HeadlessConfig.GetChromeOptions(isHeadless);
                    return new ChromeDriver(chromeOptions);

                case "firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    var firefoxOptions = HeadlessConfig.GetFirefoxOptions(isHeadless);
                    return new FirefoxDriver(firefoxOptions);

                case "edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    var edgeOptions = HeadlessConfig.GetEdgeOptions(isHeadless);
                    return new EdgeDriver(edgeOptions);

                default:
                    throw new ArgumentException($"Unsupported browser: {browser}");
            }
        }
    }
}
