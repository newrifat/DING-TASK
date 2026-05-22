using NUnit.Framework;
using DingAssignment.Pages;
using DingAssignment.Config;
using DingAssignment.Utils;
using System.Collections.Generic;

namespace DingAssignment.Tests
{
    [TestFixture]
    public class SmokeTest : BaseTest
    {
        [Test]
        public void ValidUser_ShouldLoginSuccessfully()
        {
            Driver.Navigate().GoToUrl(ConfigReader.BaseUrl);

            var loginPage = new LoginPage(Driver);
            loginPage.Login(ConfigReader.Username, ConfigReader.Password);

            WaitUtils.ForPageSync();

            Assert.That(Driver.Url, Does.Contain(UiConstants.UrlInventorySnippet), 
                "Failed to route to inventory dashboard.");
        }

        [Test]
        public void LockedOutUser_ShouldShowErrorMessage()
        {
            Driver.Navigate().GoToUrl(ConfigReader.BaseUrl);

            var loginPage = new LoginPage(Driver);
            loginPage.Login(ConfigReader.LockedOutUsername, ConfigReader.Password);

            WaitUtils.ForPageSync();

            string actualError = loginPage.GetErrorMessage();
            Assert.That(actualError, Does.Contain(UiConstants.ErrorEpicSadface),
                $"Error message did not match expectations. Found: '{actualError}'");
        }

        [Test]
        public void AddTwoProductsToCart_ShouldReflectInBadgeAndNavigate()
        {
            Driver.Navigate().GoToUrl(ConfigReader.BaseUrl);
            var loginPage = new LoginPage(Driver);
            loginPage.Login(ConfigReader.Username, ConfigReader.Password);

            WaitUtils.ForPageSync();

            var productsPage = new ProductsPage(Driver);
            Assert.That(productsPage.GetPageTitle(), Is.EqualTo(UiConstants.ProductsPageTitle));

            // Add 2 items dynamically using Config data
            productsPage.AddProductToCart(ConfigReader.ProductBikeLight);
            WaitUtils.ForActionSync();

            productsPage.AddProductToCart(ConfigReader.ProductBoltTShirt);
            WaitUtils.ForActionSync();

            string finalCartCount = productsPage.GetCartBadgeCount();
            Assert.That(finalCartCount, Is.EqualTo("2"), $"Expected 2 items in badge, but found: '{finalCartCount}'");

            productsPage.ClickShoppingCart();
            WaitUtils.ForActionSync();

            Assert.That(Driver.Url, Does.Contain(UiConstants.UrlCartSnippet), "Failed to route to cart summary page.");

            var cartPage = new CartPage(Driver);
            double baselineCartSubtotal = cartPage.GetCartCalculatedSubtotal(); 
            cartPage.ClickCheckout();
            WaitUtils.ForActionSync();

            var checkoutPage = new CheckoutPage(Driver);
            checkoutPage.FillShippingInformation(ConfigReader.FirstName, ConfigReader.LastName, ConfigReader.PostalCode);
            WaitUtils.ForActionSync();

            double actualCheckoutSubtotal = checkoutPage.GetSummarySubtotalPrice(); 
            double actualCheckoutGrandTotal = checkoutPage.GetSummaryGrandTotalPrice(); 

            Assert.Multiple(() =>
            {
                Assert.That(actualCheckoutSubtotal, Is.EqualTo(baselineCartSubtotal),
                    $"Subtotal mismatch! Cart calculated {baselineCartSubtotal}, but Final Step shows {actualCheckoutSubtotal}.");

                Assert.That(actualCheckoutGrandTotal, Is.GreaterThan(actualCheckoutSubtotal),
                    $"Grand Total calculation error! Price did not include tax additions: {actualCheckoutGrandTotal}");
            });

            checkoutPage.ClickFinish();
            WaitUtils.ForActionSync();

            Assert.Multiple(() =>
            {
                Assert.That(Driver.Url, Does.Contain(UiConstants.UrlCheckoutCompleteSnippet), "Failed to route to Confirmation Success screen.");

                string actualHeader = checkoutPage.GetConfirmationHeaderMessage();
                Assert.That(actualHeader, Is.EqualTo(UiConstants.OrderThankYouHeader), "The success header string text was altered.");

                string actualBodyText = checkoutPage.GetConfirmationBodyMessage();
                Assert.That(actualBodyText, Does.Contain(UiConstants.OrderDispatchedKeyword), "The delivery body text changed layout.");
            });
        }

        [Test]
        public void Navigation_UserCanReturnToPreviousStep_WithoutLosingSelections()
        {
            Driver.Navigate().GoToUrl(ConfigReader.BaseUrl);
            var loginPage = new LoginPage(Driver);
            loginPage.Login(ConfigReader.Username, ConfigReader.Password);

            WaitUtils.ForPageSync();

            var productsPage = new ProductsPage(Driver);
            productsPage.AddProductToCart(ConfigReader.ProductBackpack);
            WaitUtils.ForActionSync();

            productsPage.ClickShoppingCart();
            WaitUtils.ForActionSync();

            var cartPage = new CartPage(Driver);
            Assert.That(Driver.Url, Does.Contain(UiConstants.UrlCartSnippet));

            // Return backwards via UI
            cartPage.ClickContinueShopping();
            WaitUtils.ForActionSync();

            Assert.Multiple(() =>
            {
                Assert.That(Driver.Url, Does.Contain(UiConstants.UrlInventorySnippet), "Failed to navigate back to catalog.");
                Assert.That(productsPage.GetCartBadgeCount(), Is.EqualTo("1"), "Item count lost after back navigation.");
            });

            productsPage.ClickShoppingCart();
            WaitUtils.ForActionSync();
            cartPage.ClickCheckout();
            WaitUtils.ForActionSync();

            var checkoutPage = new CheckoutPage(Driver);
            checkoutPage.FillShippingInformation(ConfigReader.FirstName, ConfigReader.LastName, ConfigReader.PostalCode);
            WaitUtils.ForActionSync();
            Assert.That(Driver.Url, Does.Contain(UiConstants.UrlCheckoutStepTwoSnippet));

            // Cancel out of summary step via UI
            checkoutPage.ClickCancel();
            WaitUtils.ForActionSync();

            Assert.Multiple(() =>
            {
                Assert.That(Driver.Url, Does.Contain(UiConstants.UrlInventorySnippet), "Failed to return to catalog via cancel.");
                Assert.That(productsPage.GetCartBadgeCount(), Is.EqualTo("1"), "Item count wiped out after canceling.");
            });
        }
    }
}