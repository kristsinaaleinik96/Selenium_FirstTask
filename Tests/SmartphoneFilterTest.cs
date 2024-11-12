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
    [TestFixture("chrome")]
    internal class SmartphoneFilterTest : BaseTest
    {
        public SmartphoneFilterTest(string browser) : base(browser) { }
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

            smartphonesPage?.ClickMinPrice();
            smartphonesPage?.EnterMinPrice("5000");
            smartphonesPage?.ClickMaxPrice();
            smartphonesPage?.EnterMaxPrice("5200");
            smartphonesPage?.SelectMemory();
            smartphonesPage?.SelectFullHDResolution();
            smartphonesPage?.SelectMinResolution();
            smartphonesPage?.SelectMaxResolution();

            int totalAmount = smartphonesPage.GetTotalItems();
            Console.WriteLine($"Total amount of products is {totalAmount}");

            smartphonesPage?.VerifyEachSmartphone("Apple", 512, 1, 5000, 5200);


        }



    }
}


