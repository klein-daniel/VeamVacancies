using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using VeamVacancies.VeamVacancies.Domain.Drivers;

namespace VeamVacancies.VeamVacancies.Domain.Pages
{
    public abstract class BasePage
    {
        private readonly VeamDriver _webDriver;

        public BasePage(VeamDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public IWebElement FindElement(By locator)
        {
            return _webDriver.GetDriver().FindElement(locator);
        }

        public IWebElement? FindElementWithWait(By locator)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver.GetDriver(), TimeSpan.FromSeconds(10));
            try
            {
                return wait.Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch (NoSuchElementException ex)
            {

                Console.WriteLine($"Element - {locator} could not be found! Exception: {ex.Message}");
                return null;
            }

        }

        public IEnumerable<IWebElement>? FindElementsWithWait(By locator)
        {
            WebDriverWait wait = new(_webDriver.GetDriver(), TimeSpan.FromSeconds(10));
            try
            {
                return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            }
            catch (NoSuchElementException ex)
            {

                Console.WriteLine($"Elements with - {locator} could not be found! Exception: {ex.Message}");
                return null;
            }
        }

        public string GetCurrentUrl()
        {
            Thread.Sleep(2000); // needed as the URL does not update in time
            return _webDriver.GetDriver().Url;
        }

        public WebDriver GetDriver()
        {
            return _webDriver.GetDriver();
        }

        public IWebElement? GetOptionsByKeyword(IEnumerable<IWebElement> dropdownOptions, string value)
        {
            var options = from option in dropdownOptions
                          where option.Text.ToLower() == value.ToLower()
                          select option;
            return options.SingleOrDefault();
        }
    }
}
