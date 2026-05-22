# 🛒 SauceDemo E-Commerce Automation Framework

[![.NET](https://img.shields.io/badge/.NET-9.0-blue)](https://dotnet.microsoft.com/)
[![Selenium](https://img.shields.io/badge/Selenium-4.44-green)](https://www.selenium.dev/)
[![NUnit](https://img.shields.io/badge/NUnit-4.2-brightgreen)](https://nunit.org/)
[![Reqnroll](https://img.shields.io/badge/Reqnroll-2.3-orange)](https://reqnroll.net/)

A modern, high-performance Selenium WebDriver automation framework for end-to-end testing of [SauceDemo](https://www.saucedemo.com/), built with .NET 9.0, C#, NUnit, and Reqnroll (Gherkin/BDD). Implements a completely decoupled Page Object Model (POM) utilizing custom QA attributes for bulletproof selector stability.

---

## 📑 Table of Contents

- [Features](#-features)
- [Quick Start](#-quick-start)
- [Installation](#-installation)
- [Configuration](#-configuration)
- [Running Tests](#-running-tests)
- [Project Structure](#-project-structure)
- [Future Improvements](#-future-improvements)

---

## ✨ Features

- ✅ **Gherkin / BDD Integration** - Human-readable, business-focused test specifications using Reqnroll.
- ✅ **Data-Test Driven Locators** - Anchored exclusively to custom automation elements (`data-test`), rendering the suite immune to layout redesigns or styling refactors.
- ✅ **Separation of Concerns** - Complete physical segregation between execution flow logic, structural selectors, and text data maps.
- ✅ **High-Performance Utility Extensions** - C# extension methods injecting automated explicit wait guards natively into WebDriver commands to eliminate flakiness.
- ✅ **Decoupled Data Assertions** - Absolute removal of hardcoded text strings from test step code to allow seamless global maintenance.
- ✅ **Thread Isolation Configuration** - Flexible test runner scaffolding optimized for stable environment sync.

---

## 🚀 Quick Start

```bash
# 1. Clone and navigate to workspace root
git clone <your-repo-url>
cd DingAssignment

# 2. Restore system dependencies 
dotnet restore

# 3. Compile clean code-behind bindings
dotnet clean

# 4. Configure credentials
cp src/Config/appsettings.example.json src/Config/appsettings.json

# 5. Execute the complete BDD suite
dotnet test

# 6. Execute all feature scenarios
dotnet test

# 7. Execute with detailed console output logs
dotnet test --logger "console;verbosity=detailed"

# 8. Run a single designated scenario by name query
dotnet test --filter "Name~Happy Path"

```

---

## 📦 Installation

### Prerequisites

| Requirement | Version | Download |
|------------|---------|----------|
| .NET SDK | 9.0+ | [Download](https://dotnet.microsoft.com/download) |
| Chrome | Latest | [Download](https://www.google.com/chrome/) |
| Git | Latest | [Download](https://git-scm.com/) |


### Verify Installation

```bash
dotnet --version  # Should show 9.0.x or higher
```

---

## ⚙️ Configuration

### Setup Steps

**1. Copy configuration template:**
```bash
cp src/Config/appsettings.example.json src/Config/appsettings.json
```

**2. Edit `src/Config/appsettings.json` with your credentials:**

```json
{
  "AppSettings": {
    "BaseUrl": "https://www.saucedemo.com/",
    "Username": "<USERNAME>",
    "Password": "<PASSWORD>",
    "LockedOutUsername": "<LOCKED_OUT_USERNAME>"
  },
  "TestData": {
    "FirstName": "<FIRST_NAME>",
    "LastName": "<LAST_NAME>",
    "PostalCode": "<POSTAL_CODE>",
    "ProductBackpack": "<PRODUCT_BACKPACK>",
    "ProductBikeLight": "<PRODUCT_BIKE_LIGHT>",
    "ProductBoltTShirt": "<PRODUCT_BOLT_TSHIRT>"
  }
}
```


> ⚠️ **Security:** `appsettings.json` is gitignored. Never commit credentials!

### Key Configuration Options

| Setting | Default | Description |
|---------|---------|-------------|
| `Browser` | Chrome | Browser: Chrome, Firefox, Edge |
| `Headless` | false | Run without GUI |

---

## 🧪 Running Tests

### Basic Commands

```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"

```

### Test Modes

**Headless Mode (No Browser GUI):**
Set `"Headless": true` in `appsettings.json`

**Different Browser:**
Set `"Browser": "Firefox"` or `"Edge"` in `appsettings.json`

---

## 📁 Project Structure

```
DingAssignment/
├── Config/                      # Centralized Data Configurations
│   ├── appsettings.json         # Credentials, Environment Targets, Product Keys
│   ├── ConfigReader.cs          # Static Configuration Parsing Controller
│   └── UiConstants.cs           # Decoupled Core UI Assertion Strings
├── Drivers/                     # Driver Creation Scaffolding
│   └── WebDriverFactory.cs      # Chrome Session Factory Initialization
├── Features/                    # BDD Specifications Layer
│   └── SauceDemoStorefront.feature # Pure Gherkin Business Scenarios
├── Pages/                       # Page Object Model (POM) Layers
│   ├── BasePage.cs              # Core Web Element Inheritor Definition
│   ├── LoginPage.cs             # Login Fields and Error Verification Actions
│   ├── ProductsPage.cs          # Dynamic Catalog Selectors (data-test driven)
│   ├── CartPage.cs              # Mathematical Subtotal Collection and Redirections
│   └── CheckoutPage.cs          # Delivery Input and Step Two Final Receipts
├── Steps/                       # Reqnroll Glue Binding Code
│   └── SauceDemoSteps.cs        # Maps Gherkin Phrases via Regex directly to Pages
└── Utils/                       # Core Automation Utilities Extensions
    ├── WebDriverExtensions.cs   # Fluent Explicit Waits and Currency Parsers
    └── WaitUtils.cs             # Centralized Custom Environment Sync Delays
```

---

## 🚀 Future Improvements

Planned enhancements to make the framework more robust:

### 1. Environment Variable Support
- Add support for environment variable overrides (e.g., `USERNAME`, `PASSWORD`)
- Enable hierarchical configuration: Environment Variables → appsettings.json → defaults
- Allow credentials to be injected from CI/CD pipelines without changing config files
- Support for GitHub Secrets, Azure Key Vault, AWS Secrets Manager

### 2. CI/CD Pipeline Integration
- **GitHub Actions** - Add `.github/workflows/test.yml` for automated test execution
- **Azure DevOps** - Pipeline script is added but need to configure in AzureDevOps
- **Jenkins** - Add Jenkinsfile for continuous testing
- Store credentials securely in CI/CD secrets and pass via environment variables

### 3. Enhanced Reporting
- Video recording for failed tests
- Integration with test management tools (TestRail, Zephyr, qTest)
- Real-time dashboard for test execution monitoring

### 4. Parallel Execution
- Enable parallel test execution for faster runs
- Implement proper test isolation strategies
- Use thread-safe WebDriver instances

### 5. Cross-Browser Testing
- Cloud-based testing with BrowserStack or Sauce Labs
- Automated cross-browser compatibility matrix
- Mobile browser testing (iOS Safari, Android Chrome)
