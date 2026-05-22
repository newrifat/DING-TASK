using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DingAssignment.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            
            // Performance Optimization Arguments
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--blink-features=AutomationControlled"); 
            
            // Native Selenium Manager handles download automatically now
            return new ChromeDriver(options);
        }
    }
}