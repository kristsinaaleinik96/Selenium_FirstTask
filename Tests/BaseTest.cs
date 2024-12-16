using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using Selenium_FirstTask.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Selenium_FirstTask.Utils;

namespace Selenium_FirstTask.Tests
{
    internal class BaseTest
    {

        protected IWebDriver? driver;
        private readonly string? browser;
        private readonly bool isHeadless;
        public BaseTest(string? browser, bool isHeadless)
        {
            this.browser = browser;
            this.isHeadless = isHeadless; 
        }

        [SetUp]
        public void Initialize()
        {

            TestContext.Progress.WriteLine($"Initializing test on browser: {browser}, Headless: {isHeadless}");
            driver = WebDriverFactory.CreateWebDriver(browser, isHeadless);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.onliner.by/");
        }
        //private IWebDriver GetWebDriver(string? browser)
        //{
        //    switch(browser)
        //    {
        //        case "chrome":
        //            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
        //            return new ChromeDriver();
        //        case "firefox":
        //            new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
        //            return new FirefoxDriver();
        //        case "edge":
        //            new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
        //            return new EdgeDriver();
        //        default:
        //            throw new ArgumentException($"Unsupported browser {browser}");
        //    }
        //}

        [TearDown]
        public void CleanUp()
        {
            driver?.Quit();
        }

    }
}
