using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using wsep182.services;

namespace SeleniumTests
{
    [TestClass]
    public class RegisterSimple
    {
        public static String URL = "http://localhost:53416/";
        IWebDriver driver = new ChromeDriver("./");
        private int sleepTime = 500;

        [TestInitialize]
        public void Initialize()
        {

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(URL);
            Console.WriteLine("Opened URL");

            IWebElement login = driver.FindElement(By.Id("LoginLink"));
            login.Click();
            Thread.Sleep(sleepTime);
            IWebElement userName = driver.FindElement(By.Id("username"));
            userName.SendKeys("adminTest");
            Thread.Sleep(sleepTime);
            IWebElement password = driver.FindElement(By.Id("password"));
            password.SendKeys("123456");
            Thread.Sleep(sleepTime);
            IWebElement btnLogin = driver.FindElement(By.Id("btnLogin"));
            btnLogin.Click();
            Thread.Sleep(sleepTime);

            IWebElement initdb;
            int i = 0;
            while (i == 0)
            {
                try
                {
                    Thread.Sleep(sleepTime);
                    initdb = driver.FindElement(By.Id("initdbButton"));
                    i = 1;
                    initdb.Click();
                }
                catch (Exception)
                {
                    continue;
                }
            }
            Thread.Sleep(sleepTime);

            IWebElement logout = driver.FindElement(By.Id("LogoutLink"));
            logout.Click();
            Thread.Sleep(sleepTime);


        }

        [TestMethod]
        public void registerSimple()
        {
            IWebElement register = driver.FindElement(By.Id("RegisterLink"));
            register.Click();
            Thread.Sleep(sleepTime);
            IWebElement userName = driver.FindElement(By.Id("username"));
            userName.SendKeys("shaySimpleRegister");
            Thread.Sleep(sleepTime);
            IWebElement password1 = driver.FindElement(By.Id("password1"));
            password1.SendKeys("123456");
            Thread.Sleep(sleepTime);
            IWebElement password2 = driver.FindElement(By.Id("password2"));
            password2.SendKeys("123456");
            Thread.Sleep(sleepTime);

            IWebElement btnRegister = driver.FindElement(By.Id("btnRegister"));
            btnRegister.Click();
            Thread.Sleep(sleepTime);
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Assert.IsTrue(alertText.Contains("User successfuly added"));
            alert.Accept();
        }

        [TestMethod]
        public void registerUserAlreadyExists()
        {
            IWebElement register = driver.FindElement(By.Id("RegisterLink"));
            register.Click();
            Thread.Sleep(sleepTime);
            IWebElement userName = driver.FindElement(By.Id("username"));
            userName.SendKeys("shayAlreadyExists");
            Thread.Sleep(sleepTime);
            IWebElement password1 = driver.FindElement(By.Id("password1"));
            password1.SendKeys("123456");
            Thread.Sleep(sleepTime);
            IWebElement password2 = driver.FindElement(By.Id("password2"));
            password2.SendKeys("123456");
            Thread.Sleep(sleepTime);

            IWebElement btnRegister = driver.FindElement(By.Id("btnRegister"));
            btnRegister.Click();
            Thread.Sleep(sleepTime);
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Assert.IsTrue(alertText.Contains("User successfuly added"));
            alert.Accept();

            register = driver.FindElement(By.Id("RegisterLink"));
            register.Click();
            Thread.Sleep(sleepTime);
            userName = driver.FindElement(By.Id("username"));
            userName.SendKeys("shayAlreadyExists");
            Thread.Sleep(sleepTime);
            password1 = driver.FindElement(By.Id("password1"));
            password1.SendKeys("126");
            Thread.Sleep(sleepTime);
            password2 = driver.FindElement(By.Id("password2"));
            password2.SendKeys("126");
            Thread.Sleep(sleepTime);
            btnRegister = driver.FindElement(By.Id("btnRegister"));
            btnRegister.Click();
            Thread.Sleep(sleepTime);
            IWebElement registerAlert = driver.FindElement(By.Id("registerAlert"));
            Assert.IsTrue(registerAlert.Text.Contains("Failure - error: username allready exist in the system"));
        }

        [TestMethod]
        public void registerPasswordNotMatch()
        {
            IWebElement register;
            IWebElement userName;
            IWebElement password1;
            IWebElement password2;
            IWebElement btnRegister;

            register = driver.FindElement(By.Id("RegisterLink"));
            register.Click();
            Thread.Sleep(sleepTime);
            userName = driver.FindElement(By.Id("username"));
            userName.SendKeys("itamarr");
            Thread.Sleep(sleepTime);
            password1 = driver.FindElement(By.Id("password1"));
            password1.SendKeys("126");
            Thread.Sleep(sleepTime);
            password2 = driver.FindElement(By.Id("password2"));
            password2.SendKeys("12");
            Thread.Sleep(sleepTime);
            btnRegister = driver.FindElement(By.Id("btnRegister"));
            btnRegister.Click();
            Thread.Sleep(sleepTime);
            IWebElement registerAlert = driver.FindElement(By.Id("registerAlert"));
            Assert.IsTrue(registerAlert.Text.Contains("Failure - passwords does not match"));
        }
        [TestMethod]
        public void registerPasswordNotGood()
        {
            IWebElement register;
            IWebElement userName;

            IWebElement btnRegister;

            register = driver.FindElement(By.Id("RegisterLink"));
            register.Click();
            Thread.Sleep(sleepTime);
            userName = driver.FindElement(By.Id("username"));
            userName.SendKeys("itamarr");
            Thread.Sleep(sleepTime);
            btnRegister = driver.FindElement(By.Id("btnRegister"));
            btnRegister.Click();
            Thread.Sleep(sleepTime);
            IWebElement registerAlert = driver.FindElement(By.Id("registerAlert"));
            Assert.IsTrue(registerAlert.Text.Contains("Failure - error: password is not entered"));
        }
        [TestMethod]
        public void registerNoUserName()
        {
            IWebElement register;
            IWebElement password1;
            IWebElement password2;
            IWebElement btnRegister;

            register = driver.FindElement(By.Id("RegisterLink"));
            register.Click();
            Thread.Sleep(sleepTime);
            password1 = driver.FindElement(By.Id("password1"));
            password1.SendKeys("126");
            Thread.Sleep(sleepTime);
            password2 = driver.FindElement(By.Id("password2"));
            password2.SendKeys("126");
            Thread.Sleep(sleepTime);
            btnRegister = driver.FindElement(By.Id("btnRegister"));
            btnRegister.Click();
            Thread.Sleep(sleepTime);
            IWebElement registerAlert = driver.FindElement(By.Id("registerAlert"));
            Assert.IsTrue(registerAlert.Text.Contains("Failure - error: username is not entered"));
        }
        [TestMethod]
        public void registerUsernameOnlySpaces()
        {
            IWebElement register;
            IWebElement userName;
            IWebElement password1;
            IWebElement password2;
            IWebElement btnRegister;

            register = driver.FindElement(By.Id("RegisterLink"));
            register.Click();
            Thread.Sleep(sleepTime);
            userName = driver.FindElement(By.Id("username"));
            userName.SendKeys("        ");
            Thread.Sleep(sleepTime);
            password1 = driver.FindElement(By.Id("password1"));
            password1.SendKeys("126");
            Thread.Sleep(sleepTime);
            password2 = driver.FindElement(By.Id("password2"));
            password2.SendKeys("126");
            Thread.Sleep(sleepTime);
            btnRegister = driver.FindElement(By.Id("btnRegister"));
            btnRegister.Click();
            Thread.Sleep(sleepTime);
            IWebElement registerAlert = driver.FindElement(By.Id("registerAlert"));
            Assert.IsTrue(registerAlert.Text.Contains("Failure - error: username is not entered"));
        }
        [TestCleanup]
        public void CleanUp()
        {
            driver.Close();
            Console.WriteLine("Closed the browser");
        }

    }
}
