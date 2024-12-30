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
        public SmartphonesPage(IWebDriver? driver) : base(driver) 
        {
        }

        private IWebElement brandCheckbox => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[contains(@class, 'catalog-form__checkbox-item') and text()='Apple']/self::li")));
        private IWebElement minPriceInput => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='от']")));
        private IWebElement maxPriceInput => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder='до']")));
        private IWebElement fullResolution => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//li[contains(@class, 'catalog-form__checkbox-item') and contains(text(), 'FullHD')]")));
        private IWebElement minScreenResolutionDropdown => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[.='1080x1920']/following-sibling::select")));
        private IWebElement maxScreenResolutionDropdown => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[.='1242x2688']/following-sibling::select/option[text()='1290x2796']")));
        private IWebElement memoryCheckbox => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//li[contains(text(), 'от 512 ГБ')]")));
        private IWebElement totalItems => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(), 'Найден')]")));
        private IWebElement showNextItemsButton => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(), 'Следующие')]")));
        private IWebElement showLastItemsButton => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(), 'Последние')]")));

        private List<IWebElement> Smartphones => driver.FindElements(By.XPath("//a[(contains(@class, 'catalog-form__link catalog-form__link_primary-additional'))]")).ToList();
        private readonly By filterLoaderLocator = By.XPath("//div[@class='catalog-interaction__state catalog-interaction__state_initial catalog-interaction__state_disabled catalog-interaction__state_control']");
        private readonly string classDuringFilterLoaderApplying = "catalog-interaction__state catalog-interaction__state_initial catalog-interaction__state_disabled catalog-interaction__state_animated catalog-interaction__state_control";
        public void WaitForPageUpdate()
        {
            Logger.Info("Waiting for page updates to apply...");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(d =>
            {
                var offersElement = d.FindElement(filterLoaderLocator);
                var classAttr = offersElement.GetAttribute("class");
                return !classAttr.Contains(classDuringFilterLoaderApplying);
            });
        }
        public void SelectBrand()
        {
            Logger.Info("Selecting Brand");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", brandCheckbox);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", brandCheckbox);
            WaitForPageUpdate();
        }
        public void SelectMemory()
        {

            Logger.Info("Selecting smartphone memory");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", memoryCheckbox);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", memoryCheckbox);
            WaitForPageUpdate();
        }

        public void SelectResolution()
        {
            Logger.Info("Selecting smartphone max resolution");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", fullResolution);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", fullResolution);
            WaitForPageUpdate();
        }
        public void SelectMaxResolution()
        {
            Logger.Info("Selecting smartphone max resolution");
            IWebElement dropdown = driver.FindElement(By.XPath("//div[.='1242x2688']/following-sibling::select"));
           // ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", dropdown);
            //wait.Until(ExpectedConditions.ElementToBeClickable(dropdown));
            dropdown.Click();
            SelectElement selectElement = new SelectElement(dropdown);
            selectElement.SelectByValue("1290x2796");
            WaitForPageUpdate();
        }

        public void EnterMinPrice(string minPrice)
        {
            Logger.Info("Entering on MIN price");
            minPriceInput.SendKeys(minPrice);
            minPriceInput.SendKeys(Keys.Tab);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d => minPriceInput.GetAttribute("value") == minPrice);
            Logger.Info($"MIN price input successfully set to: {minPrice}");
            WaitForPageUpdate();
        }

        public void EnterMaxPrice(string maxPrice)
        {
            Logger.Info("Entering on MAX price");
            maxPriceInput.SendKeys(maxPrice);
            maxPriceInput.SendKeys(Keys.Tab);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d => maxPriceInput.GetAttribute("value") == maxPrice);
            Logger.Info($"MAX price input successfully set to: {maxPrice}");
            WaitForPageUpdate();
        }
        public int GetTotalItems()
        {
            Thread.Sleep(10000);
            var totalCount = totalItems.Text;
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

        public void VerifySmartphoneBrand(string expectedBrand)
        {

            Logger.Info("Verifying smartpone brand");
            foreach (var smartphone in Smartphones)
            {
                string smartphoneText = smartphone.Text;
                Assert.That(smartphoneText.Contains(expectedBrand),
                     $"Expected brand '{expectedBrand}' not found in smartphone description: {smartphoneText}");
            }
        }
        public void VerifySmartphoneMemory(string memoryFrom, string memoryUpTo)
        {
            Logger.Info("Verifying smartpone memory");
            foreach (var smartphone in Smartphones)
            {
                string smartphoneText = smartphone.Text;
                var memoryElement = smartphone.FindElement(By.XPath("//div[contains(@class, 'catalog-form__description') and contains(text(), 'память')]"));
                string memoryText = memoryElement.Text
                    .Replace("память", "")
                    .Replace("TB", "").Replace("GB", "")
                    .Replace("ТБ", "").Replace("ГБ", "")
                    .Replace(" ", "").Replace(",", "");

                Assert.That(memoryText == memoryFrom || memoryText == memoryUpTo,
                    $"Smartphone memory {memoryText} is not in the expected range 512GB - 1TB for: {smartphoneText}");
            }
        }

        public void VerifySmartphonePrice(decimal maxPrice)
        {
            Logger.Info("Verifying smartpone price");
            foreach (var smartphone in Smartphones)
            {
                string smartphoneText = smartphone.Text;
                var priceElement = smartphone.FindElement(By.XPath("//span[contains(text(), 'р.')]"));
                Logger.Info($"SmartphoneText is: {smartphoneText}");
                string priceText = priceElement.Text.Replace("р.", "").Replace(" ", "").Replace(",", ".");
                decimal price = decimal.Parse(priceText);
                Assert.That(price <= maxPrice,
                $"The price of smartphone {price} is not in the expected range ({maxPrice}) for: {smartphoneText}");
            }
        }

        public void ShowNextItems()
        {
            Logger.Info("Clicking on 'Show next items' button");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", showNextItemsButton);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", showNextItemsButton);
            WaitForPageUpdate();
        }
        public void ShowLastItems()
        {
            Logger.Info("Clicking on 'Show last items' button");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", showLastItemsButton);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", showLastItemsButton);
            WaitForPageUpdate();
        }


    }

}
