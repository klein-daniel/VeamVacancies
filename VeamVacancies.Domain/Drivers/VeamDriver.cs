using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeamVacancies.VeamVacancies.Domain.Drivers
{
    public class VeamDriver
    {
        private WebDriver? _driver;

        public WebDriver GetDriver()
        {
            if (_driver == null)
            {
                _driver = new ChromeDriver(@"D:\Repos\Veam\VeamVacancies\VeamVacancies.Domain\Drivers");
                _driver.Manage().Window.Maximize();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                _driver.Url = "https://cz.careers.veeam.com/vacancies";
            }
            return _driver;
        }
    }
}
