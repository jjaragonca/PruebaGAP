using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnical.PageObjects
{
    public class LoginView
    {
        private IWebDriver driver;

        public LoginView(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void LoginUser()
        {
            IWebElement elementUsername = driver.FindElement(By.Id("user_email"));
            // Action can be performed on Input Button element
            elementUsername.SendKeys("gap-automation-test@mailinator.com");

            IWebElement elementPassword = driver.FindElement(By.Id("user_password"));
            // Action can be performed on Input Button element
            elementPassword.SendKeys("12345678");

            IWebElement button = driver.FindElement(By.Name("commit"));
            button.Click();
        }
    }
}
