using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Selenium_FirstTask.Utils;

namespace Selenium_FirstTask.PO
{
    internal class MegaMenuCatalog : BasePage
    {

        public MegaMenuCatalog(IWebDriver? driver) : base(driver) { }
    
        private IWebElement ElectronicCatalog => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), 'Электроника')]")));
        private IWebElement SmartphonesCatalog => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(), 'Смартфоны')]")));

        public void NavigateElectronicCatalog()
        {
            Logger.Info("Navigating to Electronic catalog");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", ElectronicCatalog);

            VerifyPageTitle("Каталог");
        }
        public void NavigateSmartphonesCatalog()
        {
            Logger.Info("Navigating to Smartphones catalog");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", SmartphonesCatalog);

            VerifyPageTitle("Мобильный телефон");
        }
    }
}
