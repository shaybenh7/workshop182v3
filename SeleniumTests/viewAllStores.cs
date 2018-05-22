using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace SeleniumTests
{
    [TestClass]
    public class viewAllStores
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
                    initdb = driver.FindElement(By.Id("initdbButton"));
                    Thread.Sleep(sleepTime);
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
        public void GUIviewAllStores()
        {
            IWebElement AllStores = driver.FindElement(By.Id("AllStoresLink"));
            AllStores.Click();
            Thread.Sleep(sleepTime);
            IWebElement store0 = driver.FindElement(By.Id("storeName0"));
            Assert.IsTrue(store0.Text.Equals("Store Name: Maria&Netta Inc."));
            IWebElement storeCreator = driver.FindElement(By.Id("ownerName0"));
            Assert.IsTrue(storeCreator.Text.Contains("Store Creator: admin"));
        }
        [TestMethod]
        public void GUIviewAllStoresAndGetInTheStore()
        {
            IWebElement AllProducts = driver.FindElement(By.Id("AllStoresLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime);
            IWebElement store0 = driver.FindElement(By.Id("storeName0"));
            store0.Click();
            Thread.Sleep(sleepTime);
            IWebElement storeName = driver.FindElement(By.Id("store-name"));
            Assert.IsTrue(storeName.Text.Contains("Maria&Netta Inc."));
            IWebElement owner = driver.FindElement(By.Id("owners"));
            Assert.IsTrue(owner.Text.Contains("admin"));
            IWebElement product0 = driver.FindElement(By.Id("productName0"));
            Assert.IsTrue(product0.Text.Contains("cola"));
            IWebElement product1 = driver.FindElement(By.Id("productName1"));
            Assert.IsTrue(product1.Text.Contains("cola"));
        }
        [TestCleanup]
        public void CleanUp()
        {
            driver.Close();
            Console.WriteLine("Closed the browser");
        }
    }
}
