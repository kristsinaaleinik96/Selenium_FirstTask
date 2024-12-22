using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager.Services;
using Selenium_FirstTask.PO;


namespace Selenium_FirstTask.Tests
{
    [TestFixture("chrome", false)]
   // [TestFixture("firefox", false)]
   // [TestFixture("edge", true)]
    [Parallelizable(ParallelScope.Fixtures)]
    internal class SmartphoneFilterTest : BaseTest
    {
        public SmartphoneFilterTest(string browser, bool isHeadless) : base(browser, isHeadless) { }
        [Test]
        public void ExecuteTest()
        {
            StartPage startPage = new StartPage(driver);

            startPage?.NavigateCatalog();

            MegaMenuCatalog megaMenu = new MegaMenuCatalog(driver);
            megaMenu?.NavigateElectronicCatalog();
            megaMenu?.NavigateSmartphonesCatalog();

            SmartphonesPage smartphonesPage = new SmartphonesPage(driver);
            smartphonesPage?.SelectBrand();
            smartphonesPage?.EnterMinPrice("5000");
            smartphonesPage?.EnterMaxPrice("5200");
            smartphonesPage?.SelectMemory();
            smartphonesPage?.SelectResolution();
            smartphonesPage?.SelectMaxResolution();


            int totalAmount = smartphonesPage.GetTotalItems();
            Console.WriteLine($"Total amount of products is {totalAmount}");

            //smartphonesPage?.ShowNextItems();
            //smartphonesPage?.VerifySmartphoneBrand("Apple");
            //smartphonesPage?.VerifySmartphoneMemory("512", "1");
            //smartphonesPage?.VerifySmartphonePrice(5200);

        }
    }
}


