
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PruebaTecnical.PageObjects;
using RelevantCodes.ExtentReports;
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

                string path = AppDomain.CurrentDomain.BaseDirectory;
                string actualPath = path.Substring(0, path.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                string reportPath = projectPath + "Reports\\ReportHTML.html";

                _extent = new ExtentReports(reportPath, true);
                _extent
                .AddSystemInfo("Host Name", "My Host")
                .AddSystemInfo("Environment", "QA")
                .AddSystemInfo("User Name", "Juan Aragon");
                _extent.LoadConfig(projectPath + "extent-config.xml");
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
                _test = _extent.StartTest("FinalReport");
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
            _test.Log(LogStatus.Pass, "");
        }

        public void AssertLoggedUserTag(String label)
        {
            Assert.IsTrue(label.Trim().ToLower().Contains("welcome"));
            _test.Log(LogStatus.Pass, "");

        }

        public void AssertLoggedUserBanner(String label)
        {
            Assert.AreEqual(label.Trim().ToLower(), "signed in successfully.");
            _test.Log(LogStatus.Pass, "");

        }


        public void AssertCreatedEmployee(String label)
        {
            Assert.AreEqual(label.Trim().ToLower(), "employee was successfully created.");
            _test.Log(LogStatus.Pass, "");

        }

        [TearDown]
        public void GetResult()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
                _test.Log(LogStatus.Fail, stackTrace + errorMessage);
            }
            _extent.EndTest(_test);
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            _extent.Flush();
            _extent.Close();
        }

    }
}