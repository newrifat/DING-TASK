using OpenQA.Selenium;
using DingAssignment.Utils; // Essential import to activate your extensions

namespace DingAssignment.Pages
{
    public class CheckoutPage : BasePage
    {
        public CheckoutPage(IWebDriver driver) : base(driver) { }

        private readonly By _firstNameField = By.CssSelector("[data-test='firstName']");
        private readonly By _lastNameField = By.CssSelector("[data-test='lastName']");
        private readonly By _postalCodeField = By.CssSelector("[data-test='postalCode']");
        private readonly By _continueButton = By.CssSelector("[data-test='continue']");
        private readonly By _subtotalLabel = By.CssSelector("[data-test='subtotal-label']");
        private readonly By _totalLabel = By.CssSelector("[data-test='total-label']");
        private readonly By _finishButton = By.CssSelector("[data-test='finish']");
        private readonly By _cancelButton = By.CssSelector("[data-test='cancel']");
        private readonly By _completeHeaderLabel = By.CssSelector("[data-test='complete-header']");
        private readonly By _completeTextBody = By.CssSelector("[data-test='complete-text']");

        public void FillShippingInformation(string firstName, string lastName, string zipCode)
        {
            Driver.WaitAndEnterText(_firstNameField, firstName);
            Driver.WaitAndEnterText(_lastNameField, lastName);
            Driver.WaitAndEnterText(_postalCodeField, zipCode);
            Driver.WaitAndClick(_continueButton);
        }

        public double GetSummarySubtotalPrice()
        {
            // Fluent pipeline: Get text securely -> Parse directly into a double!
            return Driver.WaitAndGetText(_subtotalLabel).ParseCurrency();
        }

        public double GetSummaryGrandTotalPrice()
        {
            return Driver.WaitAndGetText(_totalLabel).ParseCurrency();
        }

        public void ClickFinish() => Driver.WaitAndClick(_finishButton);
        public void ClickCancel() => Driver.WaitAndClick(_cancelButton);
        public string GetConfirmationHeaderMessage() => Driver.WaitAndGetText(_completeHeaderLabel);
        public string GetConfirmationBodyMessage() => Driver.WaitAndGetText(_completeTextBody);
    }
}