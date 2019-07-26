using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PruebaTecnical.PageObjects;
using System;
using System.IO;
using System.Threading;

namespace Tests
{
    public class Tests
    {
        IWebDriver driver;
        protected ExtentReports _extent;
        protected ExtentTest _test;

        [OneTimeSetUp]
        public void BeforeClass()
        {
            try
            {
                //To create report directory and add HTML report into it

                _extent = new ExtentReports();
                var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
                DirectoryInfo di = Directory.CreateDirectory(dir + "\\Test_Execution_Reports");
                var htmlReporter = new ExtentHtmlReporter(dir + "\\Test_Execution_Reports\\Automation_Report.html");
                _extent.AddSystemInfo("Environment", "QA");
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
                driver = new ChromeDriver("C:\\");
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
            Assert.IsTrue(value);
        }

        public void AssertLoggedUserTag(String label)
        {
            Assert.IsTrue(label.Trim().ToLower().Contains("welcome"));
        }

        public void AssertLoggedUserBanner(String label)
        {
            Assert.AreEqual(label.Trim().ToLower(), "signed in successfully.");
        }


        public void AssertCreatedEmployee(String label)
        {
            Assert.AreEqual(label.Trim().ToLower(), "employee was successfully created.");
        }

        [TearDown]
        public void AfterTest()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = "" +TestContext.CurrentContext.Result.StackTrace + "";
                var errorMessage = TestContext.CurrentContext.Result.Message;
                Status logstatus;
                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        string screenShotPath = Capture(driver, TestContext.CurrentContext.Test.Name);
                        _test.Log(logstatus, "Test ended with " +logstatus + " – " +errorMessage);
                        _test.Log(logstatus, "Snapshot below: " +_test.AddScreenCaptureFromPath(screenShotPath));
                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        _test.Log(logstatus, "Test ended with " +logstatus);
                        break;
                    default:
                        logstatus = Status.Pass;
                        _test.Log(logstatus, "Test ended with " +logstatus);
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
                throw (e);
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