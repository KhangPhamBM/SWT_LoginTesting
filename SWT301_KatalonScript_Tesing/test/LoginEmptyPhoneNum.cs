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
    public class LoginEmptyPhoneNum
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
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [TestCaseSource(nameof(LoginTestLoginTestSuccessfulLoginData))]
        public void TheLoginEmptyPhoneNumTest(string phoneNumber, string password, string expected)
        {
            driver.Navigate().GoToUrl("https://yoga-center-website.vercel.app/login");
            driver.FindElement(By.Id("phoneNumber")).Click();
            driver.FindElement(By.Id("phoneNumber")).Clear();
            driver.FindElement(By.Id("phoneNumber")).SendKeys(phoneNumber);
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.XPath("//div[@id='root']/div/div/main/div/div/div/div[2]/form/div[2]/div/div/div/div/div/span")).Click();
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(1500);
            Assert.AreEqual(expected, driver.FindElement(By.XPath("//div[@class='ant-form-item-explain-error']")).Text);
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
            testCases.Add(new TestCaseData("", "123456", "Phone cannot be blank"));
            testCases.Add(new TestCaseData("", "1238956", "Phone cannot be blank"));
            testCases.Add(new TestCaseData("", "1234586", "Phone cannot be blank"));
            /*testCases.Add(new TestCaseData("", "12345886", "Phone cannot be blank"));
            testCases.Add(new TestCaseData("", "12345886", "Phone cannot be blank"));
            testCases.Add(new TestCaseData("", "12345776", "Phone cannot be blank"));
            testCases.Add(new TestCaseData("", "098761", "Phone cannot be blank"));
            testCases.Add(new TestCaseData("", "22222222", "Phone cannot be blank"));
            testCases.Add(new TestCaseData("", "12345678", "Phone cannot be blank"));
            testCases.Add(new TestCaseData("", "1234586", "Phone cannot be blank"));
            testCases.Add(new TestCaseData("", "123458", "Phone cannot be blank"));
            testCases.Add(new TestCaseData("", "12341234", "Phone cannot be blank"));
            testCases.Add(new TestCaseData("", "0909090909", "Phone cannot be blank"));
            */

            // Add more test cases as needed

            return testCases;
        }

    }
}
