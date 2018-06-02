using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using wsep182.services;

namespace SeleniumTests
{
    [TestClass]
    public class removeProductFromCart
    {
        public static String URL = "http://localhost:53416/";
        IWebDriver driver = new ChromeDriver("./");
        private int sleepTime = 2000;
        [TestInitialize]
        public void Initialize()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.insertData();
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

            //add product to cart
            IWebElement AllProducts = driver.FindElement(By.Id("AllProductsLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime*2);
            IWebElement sale1 = driver.FindElement(By.Id("viewSale0"));
            sale1.Click();
            Thread.Sleep(sleepTime*2);
            IWebElement submitViewInstantSale = driver.FindElement(By.Id("submit"));
            submitViewInstantSale.Click();
            Thread.Sleep(sleepTime*2);
            IAlert alert2 = driver.SwitchTo().Alert();
            alert2.Accept();
            Thread.Sleep(sleepTime);
            IWebElement shoppingCartIcon = driver.FindElement(By.Id("shoppingCartIcon"));
            shoppingCartIcon.Click();
            Thread.Sleep(sleepTime*2);
        }
        [TestMethod]
        public void simpleRemoveProductFromCart()
        {
            IWebElement removebtn = driver.FindElement(By.Id("remove0"));
            removebtn.Click();
            Thread.Sleep(sleepTime*2);
            IWebElement totalPrice = driver.FindElement(By.Id("total-price"));
            Assert.IsTrue(totalPrice.Text.Contains("0.00"));
        }
        [TestCleanup]
        public void CleanUp()
        {
            driver.Close();
            Console.WriteLine("Closed the browser");
        }
    
    }
}
