using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;

// Leave this in - Needed for Select 
using OpenQA.Selenium.Support.UI;


/* 
 * 
 * Notes
 * This has been tested on July 4th, 2019 against (OFFICE system)
 * FIREFOX 60.7.2esr (64-bit)
 * CHROME Version 96 (64-bit)
 * EDGE Version 96
 * IE Version 11 - ERROR Currently do not use
 * 
 * Note that in the lab we must use an older version of ChromeDriver at present.  
 * 
 * C. Mark Yendt (July 2019)
 * Updated November 2021 (EDGE/CHROME/FIREFOX should work) - do not use IE
 * Updated Decemebr 2021 with updated NuGet Drivers and Added Selenium.Support
 */

namespace A6Start
{
    [TestClass]
    public class KatalonAutomationExample
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;
        // private const string BROWSER = "FIREFOX";
       private const string BROWSER = "CHROME";
        // private const string BROWSER = "IE";
        // private const string BROWSER = "EDGE";

        private const string DRIVER_LOCATION = @"C:\D";
        private const string FIREFOX_BIN_LOCATION = @"C:\Program Files\Mozilla Firefox\firefox.exe";

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            // FIREFOX
            if (BROWSER == "FIREFOX")
            {
                //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(DRIVER_LOCATION);
                // Note that the line below needs to be the full exe Name not just the path
                // service.FirefoxBinaryPath = FIREFOX_BIN_LOCATION;
                // service.Host = "::1";
                //driver = new FirefoxDriver(service);   // WORKS 
            }
            else if (BROWSER == "CHROME")
                driver = new ChromeDriver(DRIVER_LOCATION);  // WORKS ! 
            //else if (BROWSER == "IE")
            //    // Internet EXPLORER NOTE : Must add DRIVER_LOCATION to Path
            //    driver = new InternetExplorerDriver();  // WORKS !
            //else if (BROWSER == "EDGE")
            //    driver = new EdgeDriver(DRIVER_LOCATION);


        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            try
            {
                //driver.Quit();// quit does not close the window
                driver.Close();
                driver.Dispose();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [TestInitialize]
        public void InitializeTest()
        {
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [TestMethod]
        public void RunAllTests()
        {

            // Put your test cases in order here

            TestLoginAdmin();
            // Create User
            // Delete User
            // Directory Testing - 4 Cities




        }


        public void TestLoginAdmin()
        {
            driver.Navigate().GoToUrl("https://csunix.mohawkcollege.ca/tooltime/comp10066/A3/login.php");
            //driver.FindElement(By.Id("username")).Click();
            //driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("admin");
            //driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("adminP6ss");
            driver.FindElement(By.Name("Submit")).Click();
            driver.FindElement(By.Id("loginname")).Click();
            try
            {
                Assert.AreEqual("User: admin", driver.FindElement(By.Id("loginname")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("User Admin", driver.FindElement(By.LinkText("User Admin")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.FindElement(By.Id("loginname")).Click();
            try
            {
                Assert.AreEqual("Not Logged In", driver.FindElement(By.Id("loginname")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}

