using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnical.PageObjects
{
    public class HomeView
    {
        private IWebDriver driver;

        public HomeView(IWebDriver driver)
        {
            this.driver = driver;
        }

        public Boolean GetValidationLogo()
        {

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(12000));

            try { 
                wait.Until(drv => drv.FindElement(By.CssSelector("div[id=logo]")));
                return true;
            }catch(Exception e) {
                
                return false;
                throw (e);
            }
        }
        public String GetValidationUserLoggedBanner()
        {
            IWebElement FlashContent = driver.FindElement(By.CssSelector("p[class=flash_notice]"));
            String text = FlashContent.GetProperty("innerHTML");
            return text;
        }

        public String GetValidationUserLoggedName()
        {
            IWebElement FlashContent = driver.FindElement(By.CssSelector("ul[id=user_information] > span"));
            String text = FlashContent.GetProperty("innerHTML");
            return text;

        }

        public void createUser()
        {
            IWebElement buttonNew = driver.FindElement(By.CssSelector("p > a"));
            buttonNew.Click();
        }


    }
}
