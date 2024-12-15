using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using Serilog;
using System.Xml.Linq;
using Selenium_FirstTask.Utils;

namespace Selenium_FirstTask.PO
{
    public class StartPage : BasePage
    {

        public StartPage(IWebDriver? driver) : base(driver) { }

        private IWebElement ProductCatalog => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//nav//span[contains(text(), 'Каталог')]")));
        private IWebElement CookieSubmitButton => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@id='submit-button']")));


        public void NavigateCatalog()
        {
            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", ProductCatalog);
                Logger.Info("Navigating to Catalog");
            }
            catch (Exception exception) 
            {
                Logger.Error($"Error occurs while Navigating to Catalog", exception);
                throw;
            }
            VerifyPageTitle("Каталог");
        }
        public void SubmitCookie() 
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", CookieSubmitButton);
        }
    }
}
