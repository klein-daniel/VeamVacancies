using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using VeamVacancies.VeamVacancies.Domain.Drivers;
using VeamVacancies.VeamVacancies.Domain.Pages;
using static VeamVacancies.VeamVacancies.Domain.Locators.Locators;

namespace VeamVacancies.VeamVacancies.Tests
{
    public class VacanciesTests
    {
        private VeamDriver veamDriver;
        private VacanciesPage vacanciesPage;

        private int actualNrOfRandDjobs = 13;

        [SetUp]
        public void SetUp()
        {
            veamDriver = new VeamDriver();
            vacanciesPage = new VacanciesPage(veamDriver);
        }

        [Test]
        public void Test_CalculateNumberOfRAndDJobsListed()
        {
            //Get departments dropdown and click on it
            var departments = vacanciesPage.FindElementWithWait(VacanciesPageLocators.DEPARTMENTS);
            Assert.That(departments, Is.Not.Null, $"Element identified with {VacanciesPageLocators.DEPARTMENTS} - could not be found!");
            departments.Click();

            //Get department options and select R&D
            var departmentOptions = vacanciesPage.FindElementsWithWait(VacanciesPageLocators.DEPARTMENT_OPTIONS);
            Assert.That(departmentOptions, Is.Not.Null);
          
            var researchAndDevelopment = vacanciesPage.GetOptionsByKeyword(departmentOptions, "Research & Development");
            Assert.That(researchAndDevelopment, Is.Not.Null);
            researchAndDevelopment.Click();

            //Get languages dropdown and click on it
            var languages = vacanciesPage.FindElementWithWait(VacanciesPageLocators.LANGUAGES);
            Assert.That(languages, Is.Not.Null);
            languages.Click();

            //Get language options and select English
            var availableLanguages = vacanciesPage.FindElementsWithWait(VacanciesPageLocators.LANGUAGE_OPTIONS);
            Assert.That(availableLanguages, Is.Not.Null);

            var english = vacanciesPage.GetOptionsByKeyword(availableLanguages, "English");
            Assert.That(english, Is.Not.Null);
            english.Click();

            //Get open vacancies count from the displayed job counter
            var openVacanciesCountElement = vacanciesPage.FindElementWithWait(VacanciesPageLocators.OPEN_VACANCIES_COUNT);
            Assert.That(openVacanciesCountElement, Is.Not.Null);
            openVacanciesCountElement.Click(); // a click is needed for URL to have the languages param updated

            var openVacanciesCountAsString = openVacanciesCountElement.Text;
            var resultOpenVacanciesCount = int.TryParse(openVacanciesCountAsString, out int openVacanciesCount);

            //Get open vacancies list displayed on the page
            var openVacanciesList = vacanciesPage.FindElementsWithWait(VacanciesPageLocators.OPEN_VACANCIES_LIST);
            Assert.That(openVacanciesList, Is.Not.Null);

            //select the jobs that are from R&D and count them
            var openVacanciesResearchAndDevelopment = from openVacancy in openVacanciesList
                                                      where openVacancy.Text == "Research & Development"
                                                      select openVacancy;
            var openVacanciesRAndDListCount = openVacanciesResearchAndDevelopment.Count();
            Assert.That(openVacanciesRAndDListCount, Is.EqualTo(actualNrOfRandDjobs)); // expected assertion as counting manual the jobs

            //Get URI for the query of jobs type R&D
            var actualUriRandDJobSearch = vacanciesPage.GetCurrentUrl();

            //Assert
            Assert.That(actualUriRandDJobSearch, Is.EqualTo(VacanciesPageLocators.URI_SEARCH_R_AND_D));

            //Assertion for open vacancies displayed (should change dinamically depending on the filters?) and the nr of jobs listed
            if (resultOpenVacanciesCount && (openVacanciesRAndDListCount > 0))
            {
                Assert.That(openVacanciesRAndDListCount, Is.EqualTo(openVacanciesCount),
                    $"The vacancies count {openVacanciesCount} " +
                    $"does not match the number of jobs listed - {openVacanciesRAndDListCount}");

            }
            else
            {
                Assert.Fail($"Could not parse the two values: {openVacanciesCountAsString} or {openVacanciesList}");
            }

            

        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenshot = ((ITakesScreenshot)vacanciesPage.GetDriver()).GetScreenshot();
                screenshot.SaveAsFile($"D:\\Repos\\Veam\\VeamVacancies\\VeamVacancies.Domain\\Screenshots\\" +
                    $"screenshot-{DateTime.Now.ToString("yyyy-MM-dd-HHmmss")}.jpg", ScreenshotImageFormat.Jpeg);
            }
            veamDriver.GetDriver().Close();
            veamDriver.GetDriver().Quit();
        }
    }
}
