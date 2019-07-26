using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnical.PageObjects
{
    public class UserCreationView
    {
        private IWebDriver driver;

        public UserCreationView(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void createUser()
        {
            //employee_first_name
            IWebElement employee_first_name = driver.FindElement(By.Id("employee_first_name"));
            // Action can be performed on Input Button element
            employee_first_name.SendKeys("James");

            // employee_last_name

            IWebElement employee_last_name = driver.FindElement(By.Id("employee_last_name"));
            // Action can be performed on Input Button element
            employee_last_name.SendKeys("Smith");

            //employee_email

            IWebElement employee_email = driver.FindElement(By.Id("employee_email"));
            // Action can be performed on Input Button element
            employee_email.SendKeys("email@email.com");

            // employee_identification

            IWebElement employee_identification = driver.FindElement(By.Id("employee_identification"));
            // Action can be performed on Input Button element
            employee_identification.SendKeys("1234567890");

            // employee_leader_name

            IWebElement employee_leader_name = driver.FindElement(By.Id("employee_leader_name"));
            // Action can be performed on Input Button element
            employee_leader_name.SendKeys("Juan José Aragón Campo");

            // employee_start_working_on_1i value = yyyy

            IWebElement employee_start_working_on_1i = driver.FindElement(By.Id("employee_start_working_on_1i"));
            // Action can be performed on Input Button element
            employee_start_working_on_1i.SendKeys("2014");

            // employee_start_working_on_2i value = n

            IWebElement employee_start_working_on_2i = driver.FindElement(By.Id("employee_start_working_on_2i"));
            // Action can be performed on Input Button element
            employee_start_working_on_2i.SendKeys("January");

            // employee_start_working_on_3i

            IWebElement employee_start_working_on_3i = driver.FindElement(By.Id("employee_start_working_on_3i"));
            // Action can be performed on Input Button element
            employee_start_working_on_3i.SendKeys("21");



            IWebElement commit = driver.FindElement(By.CssSelector("input[name=commit]"));
            // Action can be performed on Input Button element
            commit.Click();
        }

        public String GetResultCreation()
        {
            // p id='notice' Employee was successfully created.
            IWebElement valid = driver.FindElement(By.XPath("//p[@id='notice']"));

            String label = valid.GetAttribute("innerHTML");

            return label;
        }

    }
}
