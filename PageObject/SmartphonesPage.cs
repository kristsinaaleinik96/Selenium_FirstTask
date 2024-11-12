using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using Selenium_FirstTask.Utils;
using System.Diagnostics;

namespace Selenium_FirstTask.PO
{
    public class SmartphonesPage : BasePage
    {
        public SmartphonesPage(IWebDriver? driver) : base(driver) { }

        private IWebElement brandCheckbox => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[contains(@class, 'catalog-form__checkbox-item') and text()='Apple']/self::li")));
        private IWebElement minPriceInput => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='от']")));
        private IWebElement maxPriceInput => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='до']")));
        private IWebElement fullResolution => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//li[contains(@class, 'catalog-form__checkbox-item') and contains(text(), 'FullHD')]")));
        private IWebElement minScreenResolutionDropdown => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[.='1080x1920']/following-sibling::select")));
        private IWebElement maxScreenResolutionDropdown => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[.='1242x2688']/following-sibling::select/option[text()='1290x2796']")));
        private IWebElement memoryCheckbox => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//li[contains(text(), 'от 512 ГБ')]")));
        private IWebElement TotalItems => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(), 'Найдено')]")));

        private List<IWebElement> Smartphones => driver.FindElements(By.XPath("//a[(contains(@class, 'catalog-form__link catalog-form__link_primary-additional'))]")).ToList();
        
        public void VerifySmartphoneBrand(string smartphoneText, string expectedBrand)
        {
            Logger.Info("Verifying smartpone brand");

            Assert.That(smartphoneText.Contains(expectedBrand),
                $"Expected brand '{expectedBrand}' not found in smartphone description: {smartphoneText}");
        }
        public void VerifySmartphoneMemory(IWebElement smartphone, string smartphoneText, int memoryFrom, int memoryUpTo)
        {
            Logger.Info("Verifying smartpone memory");
            try
            {
                var memoryElement = smartphone.FindElement(By.XPath(".//div[contains(@class, 'catalog-form__description') and contains(text(), 'память')]"));
                string memoryText = memoryElement.Text.Replace("память", "").Replace("ТБ", "").Replace("ГБ", "").Replace(" ", "").Replace(",", "");
                int memory = int.Parse(memoryText);

                Assert.That(memory == memoryFrom || memory == memoryUpTo,
                   $"Smartphone memory {memory} is not in the expected range 512GB - 1TB for: {smartphoneText}");
            }
            catch (NoSuchElementException)
            {
                throw new Exception("Memory was not found: " + smartphoneText);
            }
        }

        public void VerifySmartphonePrice(IWebElement smartphone, string smartphoneText, decimal minPrice, decimal maxPrice)
        {
            Logger.Info("Verifying smartpone price");
            try
            {
                var priceElement = smartphone.FindElement(By.XPath(".//span[contains(text(), 'р.')]"));
                string priceText = priceElement.Text.Replace("р.", "").Replace(" ", "").Replace(",", "");
                decimal price = decimal.Parse(priceText);

                Assert.That(price >= minPrice && price <= maxPrice,
                   $"The price of smartphone {price} is not in the expected range ({minPrice} - {maxPrice}) for: {smartphoneText}");
            }
            catch (NoSuchElementException)
            {
                throw new Exception("Price was not found:  " + smartphoneText);
            }
        }
        public void VerifyEachSmartphone(string expectedBrand, int memoryFrom, int memoryUpTo, decimal minPrice, decimal maxPrice) 
        {
            foreach (var smartphone in Smartphones)
            {
                string smartphoneText = smartphone.Text;

                VerifySmartphoneBrand(smartphoneText, expectedBrand);
                VerifySmartphoneMemory(smartphone, smartphoneText, memoryFrom, memoryUpTo);
                VerifySmartphonePrice(smartphone, smartphoneText, minPrice, maxPrice);
            }
        }


        public void SelectBrand()
        {
                Logger.Info("Selecting Brand");
                //вынести try cath BaseElements, лучше без try catch
                // waiters класс отдельно 
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", brandCheckbox);
                ((IJavaScriptExecutor) driver).ExecuteScript("arguments[0].click();", brandCheckbox);
        }
        public void SelectFullHDResolution()
        {
            Logger.Info("Selecting FullHD resolution");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", fullResolution);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", fullResolution);
        }
        public void SelectMinResolution()
        {
            Logger.Info("Selecting MIN resolution");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", minScreenResolutionDropdown);

            new SelectElement(minScreenResolutionDropdown).SelectByText("1290x2796");

        }
        public void SelectMaxResolution()
        {
            Logger.Info("Selecting MAX resolution");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", maxScreenResolutionDropdown);
            new SelectElement(maxScreenResolutionDropdown).SelectByText("1290x2796");
        }
        public void SelectMemory()
        {
            Logger.Info("Selecting smartphone memory");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", memoryCheckbox);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", memoryCheckbox);
        }

        public void ClickMinPrice()
        {
            Logger.Info("Clicking on MIN memory");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", minPriceInput);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", minPriceInput);

        }
        public void EnterMinPrice(string minPrice)
        {
            Logger.Info("Entering MIN memory");
            minPriceInput.SendKeys(minPrice);
        }

        public void ClickMaxPrice()
        {
            Logger.Info("Entering MAX memory");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", maxPriceInput);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", maxPriceInput);

        }
        public void EnterMaxPrice(string maxPrice)
        {
            Logger.Info("Clicking on MAX memory");
            maxPriceInput.SendKeys(maxPrice);
        }
        public int GetTotalItems()
        {
            Thread.Sleep(10000);
            var totalCount = TotalItems.Text;
            var match = Regex.Match(totalCount, @"\d+");
            if (match.Success)
            {
                return int.Parse(match.Value);
            }
            else
            {
                throw new Exception("Total amount can not be found");
            }
        }
    }
}
