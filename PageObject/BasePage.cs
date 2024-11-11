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
            this.driver = driver;//здесь инициализация дайвер из класса с драйвер 
            wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));
        }
        public void VerifyPageTitle(string expectedTitle)
        {
            try
            {
                //ассерт вместо кэтч
                //можно в конструктор перенести

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
