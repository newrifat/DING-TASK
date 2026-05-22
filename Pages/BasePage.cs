using OpenQA.Selenium;

namespace DingAssignment.Pages
{
    /// <summary>
    /// Base blueprint wrapper providing underlying WebDriver initialization to all derived Page Objects.
    /// </summary>
    public class BasePage
    {
        protected readonly IWebDriver Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}