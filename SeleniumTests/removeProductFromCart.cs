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

            IWebElement register = driver.FindElement(By.Id("RegisterLink"));
            register.Click();
            Thread.Sleep(sleepTime);
            userName = driver.FindElement(By.Id("username"));
            userName.SendKeys("zahiSimpleRegister");
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
            alert.Accept();
            Thread.Sleep(sleepTime);
            login = driver.FindElement(By.Id("LoginLink"));
            login.Click();
            Thread.Sleep(sleepTime);
            userName = driver.FindElement(By.Id("username"));
            userName.SendKeys("zahiSimpleRegister");
            Thread.Sleep(sleepTime);
            password = driver.FindElement(By.Id("password"));
            password.SendKeys("123456");
            Thread.Sleep(sleepTime);
            btnLogin = driver.FindElement(By.Id("btnLogin"));
            btnLogin.Click();
            Thread.Sleep(sleepTime);
            IWebElement MystoreBtn = driver.FindElement(By.Id("MyStoresPublicLink"));
            MystoreBtn.Click();
            Thread.Sleep(sleepTime);
            IWebElement newStoreInput = driver.FindElement(By.Id("storeName"));
            newStoreInput.SendKeys("abowStore");
            Thread.Sleep(sleepTime);
            IWebElement crateStoreBtn = driver.FindElement(By.Id("createStoreButton12"));
            crateStoreBtn.Click();
            Thread.Sleep(sleepTime);
            alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(sleepTime);

            //add product to cart
            IWebElement AllProducts = driver.FindElement(By.Id("AllProductsLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime);
            IWebElement sale1 = driver.FindElement(By.Id("viewSale0"));
            sale1.Click();
            Thread.Sleep(sleepTime);
            IWebElement submitViewInstantSale = driver.FindElement(By.Id("submit"));
            submitViewInstantSale.Click();
            Thread.Sleep(sleepTime);
            IAlert alert2 = driver.SwitchTo().Alert();
            alert2.Accept();
            Thread.Sleep(sleepTime);
            IWebElement shoppingCartIcon = driver.FindElement(By.Id("shoppingCartIcon"));
            shoppingCartIcon.Click();
            Thread.Sleep(sleepTime);
        }
        [TestMethod]
        public void simpleRemoveProductFromCart()
        {
            IWebElement removebtn = driver.FindElement(By.Id("remove0"));
            removebtn.Click();
            Thread.Sleep(sleepTime);
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
