using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using wsep182.services;

namespace SeleniumTests
{
    [TestClass]
    public class AddStoreTest
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
            IAlert alert = null;

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


            IWebElement register = driver.FindElement(By.Id("RegisterLink"));
            register.Click();
            Thread.Sleep(sleepTime);
            userName = driver.FindElement(By.Id("username"));
            userName.SendKeys("shayAddStore");
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
            alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Assert.IsTrue(alertText.Contains("User successfuly added"));
            alert.Accept();

            login = driver.FindElement(By.Id("LoginLink"));
            login.Click();
            Thread.Sleep(sleepTime);
            IWebElement userName2 = driver.FindElement(By.Id("username"));
            userName2.SendKeys("shayAddStore");
            Thread.Sleep(sleepTime);
            password = driver.FindElement(By.Id("password"));
            password.SendKeys("123456");
            Thread.Sleep(sleepTime);
            btnLogin = driver.FindElement(By.Id("btnLogin"));
            btnLogin.Click();
            Thread.Sleep(sleepTime);
        }

        [TestMethod]
        public void AddStoreSimple()
        {
            IWebElement AllProducts = driver.FindElement(By.Id("MyStoresPublicLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime);
            IWebElement addStore = driver.FindElement(By.Id("storeName"));
            addStore.SendKeys("Baloga");
            Thread.Sleep(sleepTime);
            IWebElement createStoreButton12 = driver.FindElement(By.Id("createStoreButton12"));
            createStoreButton12.Click();
            Thread.Sleep(sleepTime);
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Assert.IsTrue(alertText.Contains("successfuly added"));
            alert.Accept();   
        }
        [TestMethod]
        public void AddStoreEmptyStoreName()
        {
            IWebElement AllProducts = driver.FindElement(By.Id("MyStoresPublicLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime);
            IWebElement addStore = driver.FindElement(By.Id("storeName"));
            addStore.SendKeys("        ");
            Thread.Sleep(sleepTime);
            IWebElement createStoreButton12 = driver.FindElement(By.Id("createStoreButton12"));
            createStoreButton12.Click();
            Thread.Sleep(sleepTime);
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Assert.IsTrue(alertText.Contains("error: illegal store name"));
            alert.Accept();
        }
        [TestMethod]
        public void AddStoreSpaces()
        {
            IWebElement AllProducts = driver.FindElement(By.Id("MyStoresPublicLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime);
            IWebElement createStoreButton12 = driver.FindElement(By.Id("createStoreButton12"));
            createStoreButton12.Click();
            Thread.Sleep(sleepTime);
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Assert.IsTrue(alertText.Contains("error: illegal store name"));
            alert.Accept();
        }
        [TestCleanup]
        public void CleanUp()
        {
            driver.Close();
            Console.WriteLine("Closed the browser");
        }

    }
}
