using NUnit.Framework;
using OpenQA.Selenium;
using DingAssignment.Drivers;

namespace DingAssignment.Tests
{
    public class BaseTest
    {
        // ThreadLocal ensures that if tests run in parallel, 
        // each thread gets its own completely isolated browser instance.
        protected IWebDriver Driver;

        [SetUp] // Runs BEFORE every single test case
        public void Setup()
        {
            Driver = WebDriverFactory.CreateChromeDriver();
            Driver.Manage().Window.Maximize();
        }

        [TearDown] // Runs AFTER every single test case
        public void Teardown()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
            }
        }
    }
}