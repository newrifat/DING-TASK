using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DingAssignment.Utils
{
    /// <summary>
    /// Centralized high-performance utility extensions for Selenium WebDriver interactions.
    /// Implements robust explicit wait guards to eliminate test flakiness.
    /// </summary>
    public static class WebDriverExtensions
    {
        private static WebDriverWait GetWait(IWebDriver driver, int timeoutSeconds = 10)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        }

        /// <summary>
        /// Explicitly waits for an element to exist and become visible, then clicks it.
        /// </summary>
        public static void WaitAndClick(this IWebDriver driver, By locator)
        {
            var element = GetWait(driver).Until(d => {
                var el = d.FindElement(locator);
                return (el.Displayed && el.Enabled) ? el : null;
            });
            element!.Click();
        }

        /// <summary>
        /// Explicitly waits for an input field, clears existing text, and inputs new keys safely.
        /// </summary>
        public static void WaitAndEnterText(this IWebDriver driver, By locator, string text)
        {
            var element = GetWait(driver).Until(d => d.FindElement(locator));
            element.Clear();
            element.SendKeys(text);
        }

        /// <summary>
        /// Safely grabs the text value from an element after ensuring it is visible on screen.
        /// </summary>
        public static string WaitAndGetText(this IWebDriver driver, By locator)
        {
            return GetWait(driver).Until(d => d.FindElement(locator)).Text;
        }

        /// <summary>
        /// Parses text fields containing currency indicators (e.g., "$39.98") dynamically into doubles.
        /// </summary>
        public static double ParseCurrency(this string rawText)
        {
            int dollarSignIndex = rawText.IndexOf('$');
            if (dollarSignIndex == -1) 
                throw new FormatException($"String layout is missing a valid currency symbol ($): '{rawText}'");

            string cleanPrice = rawText.Substring(dollarSignIndex + 1).Trim();
            return Convert.ToDouble(cleanPrice, CultureInfo.InvariantCulture);
        }
    }
}