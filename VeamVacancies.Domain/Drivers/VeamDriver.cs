using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                string driverPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "VeamVacancies.Domain\\Drivers");
                _driver = new ChromeDriver($"{driverPath}");
                _driver.Manage().Window.Maximize();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                _driver.Url = "https://cz.careers.veeam.com/vacancies";
            }
            return _driver;
        }
    }
}
