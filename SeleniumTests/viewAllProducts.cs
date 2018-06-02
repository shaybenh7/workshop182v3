using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using wsep182.services;

namespace SeleniumTests
{
    [TestClass]
    public class viewAllProducts
    {
        public static String URL = "http://localhost:53416/";
        IWebDriver driver = new ChromeDriver("./");
        private int sleepTime = 2000;
        [TestInitialize]
        public void Initialize()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.insertData();
            userServices.getInstance().startSession();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(URL);
            Console.WriteLine("Opened URL");
            IWebElement login = driver.FindElement(By.Id("LoginLink"));
            login.Click();
            Thread.Sleep(sleepTime);
            IWebElement userName2 = driver.FindElement(By.Id("username"));
            userName2.SendKeys("adminTest");
            Thread.Sleep(sleepTime);
            IWebElement password = driver.FindElement(By.Id("password"));
            password.SendKeys("123456");
            Thread.Sleep(sleepTime);
            IWebElement btnLogin = driver.FindElement(By.Id("btnLogin"));
            btnLogin.Click();
            Thread.Sleep(sleepTime);
            IAlert alert = null;

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
        public void GUIviewAllProducts()
        {
            IWebElement AllProducts = driver.FindElement(By.Id("AllProductsLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime*2);
            IWebElement product0 = driver.FindElement(By.Id("productName0"));
            Assert.IsTrue(product0.Text.Contains("cola"));
            IWebElement product1 = driver.FindElement(By.Id("productName1"));
            Assert.IsTrue(product1.Text.Contains("sprit"));
        }
        [TestMethod]
        public void GUIviewAllProductsAndGetInTheProduct()
        {
            IWebElement AllProducts = driver.FindElement(By.Id("AllProductsLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime);
            IWebElement product0 = driver.FindElement(By.Id("viewSale0"));
            product0.Click();
            Thread.Sleep(sleepTime*2);
            IWebElement productName = driver.FindElement(By.Id("productName"));
            Assert.IsTrue(productName.Text.Contains("cola"));
            IWebElement storeName = driver.FindElement(By.Id("storeName"));
            Assert.IsTrue(storeName.Text.Contains("abowim"));
            IWebElement price = driver.FindElement(By.Id("salePrice"));
            Assert.IsTrue(price.Text.Contains("100"));
        }
        [TestCleanup]
        public void CleanUp()
        {
            driver.Close();
            Console.WriteLine("Closed the browser");
        }
    }
}
