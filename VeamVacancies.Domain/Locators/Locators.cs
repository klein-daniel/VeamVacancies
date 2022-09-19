using OpenQA.Selenium;

namespace VeamVacancies.VeamVacancies.Domain.Locators
{
    public static class Locators
    {
        public static class VacanciesPageLocators
        {
            public static By VACANCIES_PAGE_TRAIT = By.XPath("/div[@class='container container-spacer-lg']/h3[contains(., 'Vacancies')]");
            public static By DEPARTMENTS = By.XPath("//button[@id=\"sl\" and contains(. ,'All departments')]");
            public static By DEPARTMENT_OPTIONS = By.XPath("//div[@class='dropdown-menu show']/a[@role=\"button\"]");
            public static By LANGUAGES = By.XPath("//button[@id='sl' and contains(., 'All languages')]");
            public static By LANGUAGE_OPTIONS = By.XPath("//div[@class='custom-control custom-checkbox']/label[@class=\"custom-control-label\"]");
            public static By OPEN_VACANCIES_COUNT = By.XPath("//h3/span[@class='text-secondary pl-2']");
            public static By OPEN_VACANCIES_LIST = By.XPath("//div[@class='card-header pb-2']/span[@class='text-warning' or @class='text-placeholder']");
            public static string URI_SEARCH_R_AND_D = "https://cz.careers.veeam.com/vacancies?query=&department=development&tag=&languages=English";
        }
    }
}
