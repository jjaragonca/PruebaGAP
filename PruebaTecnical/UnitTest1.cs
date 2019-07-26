
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PruebaTecnical.PageObjects;
using System;
using System.IO;
using System.Threading;
using System.Security;
using System.Security.Authentication;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Net.NetworkInformation;

namespace Tests
{
    public class Tests
    {
        IWebDriver driver;
        String dir;
              protected ExtentReports _extent;
            protected ExtentTest _test;

        [OneTimeSetUp]
        public void BeforeClass()
        {
            try
            {
                //To create report directory and add HTML report into it

                _extent = new ExtentReports();
                dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
                DirectoryInfo di = Directory.CreateDirectory(dir + "\\Test_Execution_Reports");
                
                var htmlReporter = new ExtentHtmlReporter(dir + "\\Test_Execution_Reports" + "\\Automation_Report" + ".html");
                _extent.AddSystemInfo("Environment", "Journey of Quality");
                _extent.AddSystemInfo("User Name", "Juan");
                _extent.AttachReporter(htmlReporter);
            }
            catch (Exception e)
            {
                throw (e);
            }

           
        }

        [SetUp]
        public void BeforeTest()
        {
            try
            {
                _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);

                driver = new ChromeDriver(dir + "..\\chromedriver");
                driver.Url = "https://vacations-management.herokuapp.com/users/sign_in";
                driver.Manage().Window.Maximize();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }


        [Test]
        public void Test1()
        {
            LoginView objLogin = new LoginView(driver);
            objLogin.LoginUser();

            HomeView objHome = new HomeView(driver);

            AssertExistsLogo(objHome.GetValidationLogo());
            AssertLoggedUserTag(objHome.GetValidationUserLoggedName());
            AssertLoggedUserBanner(objHome.GetValidationUserLoggedBanner());
            objHome.createUser();

            UserCreationView objUserCreation = new UserCreationView(driver);
            objUserCreation.createUser();

            AssertCreatedEmployee(objUserCreation.GetResultCreation());

        }

        public void AssertExistsLogo(Boolean value)
        {
            Assert.IsTrue(value,"The logo did not find");
        }

        public void AssertLoggedUserTag(String label)
        {
            Assert.IsTrue(label.Trim().ToLower().Contains("welcome"), "The name of the user did not have the format");

        }

        public void AssertLoggedUserBanner(String label)
        {
            Assert.AreEqual(label.Trim().ToLower(), "signed in successfully.","Banner did not find");

        }


        public void AssertCreatedEmployee(String label)
        {
            Assert.AreEqual(label.Trim().ToLower(), "employee was successfully created.", "Employee was not created");

        }

        [TearDown]
        public void AfterTest()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = "" +TestContext.CurrentContext.Result.StackTrace + "";
                var errorMessage = TestContext.CurrentContext.Result.Message;
                switch (status)
                {
                    case TestStatus.Failed:
                        string screenShotPath = Capture(driver, TestContext.CurrentContext.Test.Name);
                        _test.Log(Status.Fail, "Test ended with Fail" + " – " +errorMessage);
                        _test.Log(Status.Fail, "Snapshot below: " +_test.AddScreenCaptureFromPath(screenShotPath));
                        break;
                    case TestStatus.Skipped:
                        _test.Log(Status.Skip, "Test ended with Skipped");
                        break;
                    default:
                        _test.Log(Status.Pass, "Test ended with Succesful" );
                        break;
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        ///To flush extent report
        ///To quit driver instance
        /// /// Author: Sanoj
        /// Since: 23-Sep-2018
        [OneTimeTearDown]
        public void AfterClass()
        {
            try
            {
                _extent.Flush();
            }
            catch (Exception e)
            {
                throw (e); // 
            }
            driver.Quit();
        }

        /// To capture the screenshot for extent report and return actual file path
        /// Author: Sanoj
        /// Since: 23-Sep-2018
        private string Capture(IWebDriver driver, string screenShotName)
        {
            string localpath = "";
            try
            {
                Thread.Sleep(4000);
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
                DirectoryInfo di = Directory.CreateDirectory(dir + "\\Defect_Screenshots\\");
                string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "\\Defect_Screenshots\\" +screenShotName + ".png";
                localpath = new Uri(finalpth).LocalPath;
                screenshot.SaveAsFile(localpath);
            }
            catch (Exception e)
            {
                throw (e);
            }
            return localpath;
        }

    }
}