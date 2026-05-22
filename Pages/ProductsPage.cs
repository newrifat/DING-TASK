using OpenQA.Selenium;
using DingAssignment.Utils; // Essential import to activate your utilities

namespace DingAssignment.Pages
{
    /// <summary>
    /// Handles all locators and interaction logic for the SauceDemo Catalog/Products Page.
    /// Uses dynamic data-test attribute generation driven by high-performance extensions.
    /// </summary>
    public class ProductsPage : BasePage
    {
        public ProductsPage(IWebDriver driver) : base(driver) { }

        #region Locators

        private readonly By _pageHeaderTitle = By.CssSelector("[data-test='title']");
        private readonly By _shoppingCartLink = By.CssSelector("[data-test='shopping-cart-link']");
        private readonly By _shoppingCartBadge = By.CssSelector("[data-test='shopping-cart-badge']");

        #endregion

        #region Actions

        public string GetPageTitle()
        {
            return Driver.WaitAndGetText(_pageHeaderTitle);
        }

        public void AddProductToCart(string productDataTestValue)
        {
            // Dynamically build the locator and click it via our utility extension
            By dynamicLocator = By.CssSelector($"[data-test='{productDataTestValue}']");
            Driver.WaitAndClick(dynamicLocator);
        }

        public string GetCartBadgeCount()
        {
            try
            {
                // We don't use WaitAndGetText here because if the cart is empty, 
                // the badge element doesn't exist on the DOM, and we want an immediate catch.
                return Driver.FindElement(_shoppingCartBadge).Text;
            }
            catch (NoSuchElementException)
            {
                return "0";
            }
        }

        public void ClickShoppingCart()
        {
            Driver.WaitAndClick(_shoppingCartLink);
        }

        #endregion
    }
}