using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class LoginSuccess
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
                Thread.Sleep(5000);
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheLoginSuccessTest()
        {
            driver.Navigate().GoToUrl("https://yoga-center-website.vercel.app/login");
            driver.FindElement(By.Id("phoneNumber")).Click();
            driver.FindElement(By.Id("phoneNumber")).Clear();
            driver.FindElement(By.Id("phoneNumber")).SendKeys("0912356782");
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("123456");
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//*[name()='path' and contains(@d,'M942.2 486')]")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.XPath("//div[@id='root']/div/div/main/div/div/div/div[2]/form/div[2]/div/div/div/div/div/span")).Click();
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Assert.AreEqual("Loading...", driver.FindElement(By.Id("swal2-title")).Text);
            Thread.Sleep(2500);
            Assert.AreEqual("Log In Successfully", driver.FindElement(By.Id("swal2-title")).Text);
            
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
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }

        public static IEnumerable<TestCaseData> LoginTestLoginTestSuccessfulLoginData()
        {
            List<TestCaseData> testCases = new List<TestCaseData>();
            testCases.Add(new TestCaseData("0912356782", "123456", "Log In Successfully"));
            testCases.Add(new TestCaseData("0912345682", "123456", "Log In Successfully"));
            testCases.Add(new TestCaseData("0912345679", "123456", "Log In Successfully"));
            /*testCases.Add(new TestCaseData("0912345680", "123456", "Log In Successfully"));
            testCases.Add(new TestCaseData("0912345681", "123456", "Log In Successfully"));
            testCases.Add(new TestCaseData("0912345682", "123456", "Log In Successfully"));
            testCases.Add(new TestCaseData("0912345683", "123456", "Log In Successfully"));
            testCases.Add(new TestCaseData("0123456725", "123456", "Log In Successfully"));
            testCases.Add(new TestCaseData("0123456731", "123456", "Log In Successfully"));
            */

            // Add more test cases as needed

            return testCases;
        }
    }
}
