using System;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class LoginEmptyPassword
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "https://www.google.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                Thread.Sleep(4000); 
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
     
        [TestCaseSource(nameof(LoginTestEmptyPasswordData))]
        public void TheLoginEmptyPasswordTest(string phonenumber, string password, string expected)
        {
            driver.Navigate().GoToUrl("https://yoga-center-website.vercel.app/login");
            driver.FindElement(By.Id("phoneNumber")).Click();
            driver.FindElement(By.Id("phoneNumber")).Clear();
            driver.FindElement(By.Id("phoneNumber")).SendKeys(phonenumber);
            Thread.Sleep(1000);
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.Id("password")).Clear(); // Clear the password field
            driver.FindElement(By.Id("password")).SendKeys(password); // Enter an empty password
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click(); // Use a more specific locator for the login button
            Thread.Sleep(1000); // Add a small delay to allow time for error validation
            Assert.AreEqual(expected, driver.FindElement(By.CssSelector("div.ant-form-item-explain-error")).Text);
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

        public static IEnumerable<TestCaseData> LoginTestEmptyPasswordData()
        {
            List<TestCaseData> testCases = new List<TestCaseData>();
            testCases.Add(new TestCaseData("0366967957", "", "Password cannot be blank"));
            testCases.Add(new TestCaseData("0366967958", "", "Password cannot be blank"));
            testCases.Add(new TestCaseData("", "", "Phone cannot be blank"));

          /*testCases.Add(new TestCaseData("0366967957", "", "Password cannot be blank"));
             testCases.Add(new TestCaseData("0366967958", "", "Password cannot be blank"));
             testCases.Add(new TestCaseData("0366967958", "", "Password cannot be blank"));
            // Add more test cases as needed
          */
            return testCases;
        }
    
    }
}
