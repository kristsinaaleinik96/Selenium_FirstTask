using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using Selenium_FirstTask.Utils;


namespace Selenium_FirstTask.PO
{
    public abstract class BasePage
    {

        protected IWebDriver driver;
        protected WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));
            wait.Until(driver =>
            {
                string readyState = ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString();
                return readyState == "complete";
            });
        }
        public void VerifyPageTitle(string expectedTitle)
        {
            try
            {
                wait.Until(ExpectedConditions.TitleContains(expectedTitle));
                Logger.Info($"Title is correct and contains {expectedTitle} ");
            }
            catch (Exception exception)
            {
                Logger.Error("Error occurs while finding expected title {expectedTitle}", exception);
                throw;
            }
        }
    }
}
