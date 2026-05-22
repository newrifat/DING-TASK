using OpenQA.Selenium;
using DingAssignment.Utils; // Essential import to activate your utilities
using System;
using System.Collections.Generic;
using System.Linq;

namespace DingAssignment.Pages
{
    /// <summary>
    /// Handles all locators and verification actions for the SauceDemo Shopping Cart Page.
    /// </summary>
    public class CartPage : BasePage
    {
        public CartPage(IWebDriver driver) : base(driver) { }

        #region Locators

        private readonly By _cartItemNames = By.CssSelector("[data-test='inventory-item-name']");
        private readonly By _cartItemPrices = By.CssSelector("[data-test='inventory-item-price']");
        private readonly By _checkoutButton = By.CssSelector("[data-test='checkout']");
        private readonly By _continueShoppingButton = By.CssSelector("[data-test='continue-shopping']");

        #endregion

        #region Actions

        public List<string> GetCartItemNames()
        {
            // Wait for elements using standard driver controls, then map to string list
            var elements = Driver.FindElements(_cartItemNames);
            return elements.Select(element => element.Text).ToList();
        }

        public double GetCartCalculatedSubtotal()
        {
            var priceElements = Driver.FindElements(_cartItemPrices);
            double sum = 0;

            foreach (var element in priceElements)
            {
                // Pass text into our centralized ParseCurrency extension helper!
                sum += element.Text.ParseCurrency();
            }

            return sum;
        }

        public void ClickCheckout()
        {
            Driver.WaitAndClick(_checkoutButton);
        }

        public void ClickContinueShopping()
        {
            Driver.WaitAndClick(_continueShoppingButton);
        }

        #endregion
    }
}