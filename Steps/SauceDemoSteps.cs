using NUnit.Framework;
using Reqnroll;
using DingAssignment.Pages;
using DingAssignment.Config;
using DingAssignment.Utils;
using DingAssignment.Drivers;
using OpenQA.Selenium;

namespace DingAssignment.Steps
{
    [Binding]
    public class SauceDemoSteps
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;
        private ProductsPage _productsPage;
        private CartPage _cartPage;
        private CheckoutPage _checkoutPage;
        private double _baselineCartSubtotal;

        [BeforeScenario]
        public void SetupScenario()
        {
            _driver = WebDriverFactory.CreateChromeDriver();
            _driver.Manage().Window.Maximize();
            
            _loginPage = new LoginPage(_driver);
            _productsPage = new ProductsPage(_driver);
            _cartPage = new CartPage(_driver);
            _checkoutPage = new CheckoutPage(_driver);
        }

        [AfterScenario]
        public void TeardownScenario()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }

        [Given(@"the user navigates to the login page")]
        public void GivenTheUserNavigatesToTheLoginPage() => _driver.Navigate().GoToUrl(ConfigReader.BaseUrl);

        [When(@"the user logs in with valid credentials")]
        public void WhenTheUserLogsInWithValidCredentials()
        {
            _loginPage.Login(ConfigReader.Username, ConfigReader.Password);
            WaitUtils.ForPageSync();
        }

        [When(@"the user logs in using the locked out profile credentials")]
        public void WhenTheUserLogsInUsingTheLockedOutProfileCredentials()
        {
            _loginPage.Login(ConfigReader.LockedOutUsername, ConfigReader.Password);
            WaitUtils.ForPageSync();
        }

        [Then(@"the user should be routed to the inventory dashboard")]
        public void ThenTheUserShouldBeRoutedToTheInventoryDashboard()
        {
            Assert.That(_driver.Url, Does.Contain(UiConstants.UrlInventorySnippet));
        }

        [Then(@"a secure validation error container displaying the epic sadface message must appear")]
        public void ThenASecureValidationErrorContainerMustAppear()
        {
            Assert.That(_loginPage.GetErrorMessage(), Does.Contain(UiConstants.ErrorEpicSadface));
        }

        [When(@"the user adds the Bike Light and Bolt T-Shirt products to the cart")]
        public void WhenTheUserAddsProductsToTheCart()
        {
            _productsPage.AddProductToCart(ConfigReader.ProductBikeLight);
            WaitUtils.ForActionSync();
            _productsPage.AddProductToCart(ConfigReader.ProductBoltTShirt);
            WaitUtils.ForActionSync();
        }

        [When(@"the user adds a Backpack item to the cart")]
        public void WhenTheUserAddsABackpackItemToTheCart()
        {
            _productsPage.AddProductToCart(ConfigReader.ProductBackpack);
            WaitUtils.ForActionSync();
        }

        [When(@"the shopping cart badge should display ""(.*)""")]
        [Then(@"the shopping cart badge should display ""(.*)""")]
        public void ThenTheShoppingCartBadgeShouldDisplay(string expectedCount)
        {
            Assert.That(_productsPage.GetCartBadgeCount(), Is.EqualTo(expectedCount));
        }

        [When(@"the user navigates into the shopping cart page")]
        public void WhenTheUserNavigatesIntoTheShoppingCartPage()
        {
            _productsPage.ClickShoppingCart();
            WaitUtils.ForActionSync();
            Assert.That(_driver.Url, Does.Contain(UiConstants.UrlCartSnippet));
        }

        [When(@"the user completes the customer info phase")]
        public void WhenTheUserCompletesTheCustomerInfoPhase()
        {
            _baselineCartSubtotal = _cartPage.GetCartCalculatedSubtotal();
            _cartPage.ClickCheckout();
            WaitUtils.ForActionSync();
            _checkoutPage.FillShippingInformation(ConfigReader.FirstName, ConfigReader.LastName, ConfigReader.PostalCode);
            WaitUtils.ForActionSync();
        }

        [Then(@"the checkout overview pricing values must match the baseline cart totals")]
        public void ThenTheCheckoutOverviewPricingValuesMustMatchTheBaselineCartTotals()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_checkoutPage.GetSummarySubtotalPrice(), Is.EqualTo(_baselineCartSubtotal));
                Assert.That(_checkoutPage.GetSummaryGrandTotalPrice(), Is.GreaterThan(_checkoutPage.GetSummarySubtotalPrice()));
            });
        }

        [When(@"the user finalizes the transaction order")]
        public void WhenTheUserFinalizesTheTransactionOrder()
        {
            _checkoutPage.ClickFinish();
            WaitUtils.ForActionSync();
        }

        [Then(@"the checkout complete landing confirmation banners must show successfully")]
        public void ThenTheCheckoutCompleteLandingConfirmationBannersMustShowSuccessfully()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_driver.Url, Does.Contain(UiConstants.UrlCheckoutCompleteSnippet));
                Assert.That(_checkoutPage.GetConfirmationHeaderMessage(), Is.EqualTo(UiConstants.OrderThankYouHeader));
                Assert.That(_checkoutPage.GetConfirmationBodyMessage(), Does.Contain(UiConstants.OrderDispatchedKeyword));
            });
        }

        [When(@"the user chooses to continue shopping back to the catalog")]
        public void WhenTheUserChoosesToContinueShoppingBackToTheCatalog()
        {
            _cartPage.ClickContinueShopping();
            WaitUtils.ForActionSync();
        }

        [Then(@"the user should be back on the inventory layout and the badge count must still be ""(.*)""")]
        public void ThenTheUserShouldBeBackOnTheInventoryLayoutAndTheBadgeCountMustStillBe(string expectedCount)
        {
            Assert.Multiple(() =>
            {
                Assert.That(_driver.Url, Does.Contain(UiConstants.UrlInventorySnippet));
                Assert.That(_productsPage.GetCartBadgeCount(), Is.EqualTo(expectedCount));
            });
        }

        [When(@"the user moves back to checkout step two overview")]
        public void WhenTheUserMovesBackToCheckoutStepTwoOverview()
        {
            _productsPage.ClickShoppingCart();
            WaitUtils.ForActionSync();
            _cartPage.ClickCheckout();
            WaitUtils.ForActionSync();
            _checkoutPage.FillShippingInformation(ConfigReader.FirstName, ConfigReader.LastName, ConfigReader.PostalCode);
            WaitUtils.ForActionSync();
        }

        [When(@"the user cancels the summary checkout grid")]
        public void WhenTheUserCancelsTheSummaryCheckoutGrid()
        {
            _checkoutPage.ClickCancel();
            WaitUtils.ForActionSync();
        }
    }
}