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
    [TestFixture("firefox")]
    [TestFixture("edge")]
    [Parallelizable(ParallelScope.Fixtures)]
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
            smartphonesPage?.EnterMaxPrice("5200");
            smartphonesPage?.SelectMemory();


            int totalAmount = smartphonesPage.GetTotalItems();
            Console.WriteLine($"Total amount of products is {totalAmount}");

            smartphonesPage?.ShowNextItems();
            smartphonesPage?.VerifySmartphoneBrand("Apple");
            smartphonesPage?.VerifySmartphoneMemory("512", "1");
            smartphonesPage?.VerifySmartphonePrice(5200);

            smartphonesPage?.ShowNextItems();
            smartphonesPage?.VerifySmartphoneBrand("Apple");
            smartphonesPage?.VerifySmartphoneMemory("512", "1");
            smartphonesPage?.VerifySmartphonePrice(5200);

            smartphonesPage?.ShowNextItems();
            smartphonesPage?.VerifySmartphoneBrand("Apple");
            smartphonesPage?.VerifySmartphoneMemory("512", "1");
            smartphonesPage?.VerifySmartphonePrice(5200);
        }
    }
}


