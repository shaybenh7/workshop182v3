using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using wsep182.services;

namespace SeleniumTests
{
    [TestClass]
    public class removeSallFromStore
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

            //add productToStore
            IWebElement crateStoreBtn2 = driver.FindElement(By.Id("addProductInStore0"));
            crateStoreBtn2.Click();
            Thread.Sleep(sleepTime);
            IWebElement productName = driver.FindElement(By.Id("product-name"));
            productName.SendKeys("colaTests");
            Thread.Sleep(sleepTime);

            IWebElement productPrice = driver.FindElement(By.Id("product-price"));
            productPrice.SendKeys("100");
            Thread.Sleep(sleepTime);

            IWebElement productAmount = driver.FindElement(By.Id("product-amount"));
            productAmount.SendKeys("20");
            Thread.Sleep(sleepTime);

            IWebElement productCat = driver.FindElement(By.Id("product-cat"));
            productCat.SendKeys("DRINKS");
            Thread.Sleep(sleepTime);

            IWebElement productBtn = driver.FindElement(By.Id("add_product_btn"));
            productBtn.Click();
            Thread.Sleep(sleepTime);

            IAlert alert2 = driver.SwitchTo().Alert();
            alert2.Accept();
            Thread.Sleep(sleepTime);

            IWebElement MystoreBtn2 = driver.FindElement(By.Id("MyStoresPublicLink"));
            MystoreBtn2.Click();
            Thread.Sleep(sleepTime);
            IWebElement addSaleToStoreBtn = driver.FindElement(By.Id("addSaleToStore0"));
            addSaleToStoreBtn.Click();
            Thread.Sleep(sleepTime);
            IWebElement amount = driver.FindElement(By.Id("product-amount-in-sale2"));
            amount.SendKeys("2");
            Thread.Sleep(sleepTime);
            IWebElement date = driver.FindElement(By.Id("product-due-date2"));
            date.SendKeys("31/05/2018");
            Thread.Sleep(sleepTime);
            IWebElement addSale = driver.FindElement(By.Id("AddSaleBtn"));
            addSale.Click();
            Thread.Sleep(sleepTime);
            IAlert alert3 = driver.SwitchTo().Alert();
            alert3.Accept();
            Thread.Sleep(sleepTime);


        }


        [TestMethod]
        public void simpleRemoveSale()
        {
            IWebElement removeSaleFromStore = driver.FindElement(By.Id("removeSaleFromStore0"));
            removeSaleFromStore.Click();
            Thread.Sleep(sleepTime);
            IWebElement productName = driver.FindElement(By.Id("Sale-id6"));
            productName.SendKeys("3");
            Thread.Sleep(sleepTime);
            IWebElement removeSallBtn = driver.FindElement(By.Id("aviad-Remove-Sale"));
            removeSallBtn.Click();
            Thread.Sleep(sleepTime);

            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Assert.IsTrue(alertText.Contains("successfully"));
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
