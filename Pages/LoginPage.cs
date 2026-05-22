using OpenQA.Selenium;
using DingAssignment.Utils; // Essential import to activate your extensions

namespace DingAssignment.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        private readonly By _usernameField = By.CssSelector("input[data-test='username']");
        private readonly By _passwordField = By.CssSelector("input[data-test='password']");
        private readonly By _loginButton = By.CssSelector("input[data-test='login-button']");
        private readonly By _errorMessage = By.CssSelector("h3[data-test='error']");

        public void Login(string username, string password)
        {
            // Call utilities directly on the Driver instance fluently!
            Driver.WaitAndEnterText(_usernameField, username);
            Driver.WaitAndEnterText(_passwordField, password);
            Driver.WaitAndClick(_loginButton);
        }

        public string GetErrorMessage()
        {
            return Driver.WaitAndGetText(_errorMessage);
        }
    }
}