using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using wsep182.services;

namespace SeleniumTests
{
    [TestClass]
    public class AddProductToStore
    {
        public static String URL = "http://localhost:53416/";
        IWebDriver driver = new ChromeDriver("./");
        private int sleepTime = 2000;
        [TestInitialize]
        public void Initialize()
        {
            WebServices.DAL.CleanDB cDB = new WebServices.DAL.CleanDB();
            cDB.emptyDB();
            cDB.addUserToDB("zahiSimpleRegister", "123456");
            cDB.addUserToDB("aviadTest", "123456");
            cDB.addStoreToDB("zahiSimpleRegister", "abowStore");
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


            IWebElement login1 = driver.FindElement(By.Id("LoginLink"));
            login1.Click();
            Thread.Sleep(sleepTime);
            IWebElement userName = driver.FindElement(By.Id("username"));
            userName.SendKeys("zahiSimpleRegister");
            Thread.Sleep(sleepTime);
            IWebElement password3 = driver.FindElement(By.Id("password"));
            password3.SendKeys("123456");
            Thread.Sleep(sleepTime);
            IWebElement btnLogin3 = driver.FindElement(By.Id("btnLogin"));
            btnLogin3.Click();
            Thread.Sleep(sleepTime);
            IWebElement MystoreBtn = driver.FindElement(By.Id("MyStoresPublicLink"));
            MystoreBtn.Click();
            Thread.Sleep(sleepTime * 2);

        }

        [TestMethod]
        public void simpleAddProduct()
        {
            IWebElement crateStoreBtn = driver.FindElement(By.Id("addProductInStore0"));
            crateStoreBtn.Click();
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
            Thread.Sleep(sleepTime*3);

            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Assert.IsTrue(alertText.Contains("successfuly added"));
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
