Feature: SauceDemo Core Storefront Operations
  As a retail customer
  I want to interact with the catalog, add products, and manage my cart layout safely
  So that I can verify e-commerce flow operations work perfectly

  Background:
    Given the user navigates to the login page

  Scenario: Happy Path - User completes an End-to-End purchase
    When the user logs in with valid credentials
    Then the user should be routed to the inventory dashboard
    And the user adds the Bike Light and Bolt T-Shirt products to the cart
    And the shopping cart badge should display "2"
    When the user navigates into the shopping cart page
    And the user completes the customer info phase
    Then the checkout overview pricing values must match the baseline cart totals
    When the user finalizes the transaction order
    Then the checkout complete landing confirmation banners must show successfully

  Scenario: Negative Scenarios & Validation - User submits invalid input and sees error message
    When the user logs in using the locked out profile credentials
    Then a secure validation error container displaying the epic sadface message must appear

  Scenario: Navigation - User can return to a previous step without losing their selection
    When the user logs in with valid credentials
    And the user adds a Backpack item to the cart
    And the user navigates into the shopping cart page
    And the user chooses to continue shopping back to the catalog
    Then the user should be back on the inventory layout and the badge count must still be "1"
    When the user moves back to checkout step two overview
    And the user cancels the summary checkout grid
    Then the user should be back on the inventory layout and the badge count must still be "1"