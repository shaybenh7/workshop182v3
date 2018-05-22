using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using wsep182.services;

namespace SeleniumTests
{
    [TestClass]
    public class AddProductToCartTest
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

            Thread.Sleep(sleepTime);

            IWebElement initdb;
            int i = 0;
            while (i == 0) {
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
        public void GUIaddProductToCart()
        {
            IWebElement AllProducts = driver.FindElement(By.Id("AllProductsLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime);
            IWebElement sale1 = driver.FindElement(By.Id("viewSale0"));
            sale1.Click();
            Thread.Sleep(sleepTime);
            IWebElement submitViewInstantSale = driver.FindElement(By.Id("submit"));
            submitViewInstantSale.Click();
            Thread.Sleep(sleepTime);
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Assert.IsTrue(alertText.Contains("Product was added successfully!"));
            alert.Accept();
            Thread.Sleep(sleepTime);
            IWebElement shoppingCartIcon = driver.FindElement(By.Id("shoppingCartIcon"));
            shoppingCartIcon.Click();
            Thread.Sleep(sleepTime);
            IWebElement productInCart = driver.FindElement(By.Id("productName0"));
            Assert.IsTrue(productInCart.Text.Equals("Milk chocolate"));
        }

        [TestMethod]
        public void GUIaddProductToCartIncreaseQuantity()
        {
            IWebElement AllProducts = driver.FindElement(By.Id("AllProductsLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime);
            IWebElement sale1 = driver.FindElement(By.Id("viewSale0"));
            sale1.Click();
            Thread.Sleep(sleepTime);
            IWebElement upQuan = driver.FindElement(By.Id("up-bar"));
            upQuan.Click();
            Thread.Sleep(sleepTime);
            IWebElement submitViewInstantSale = driver.FindElement(By.Id("submit"));
            submitViewInstantSale.Click();
            Thread.Sleep(sleepTime);
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Assert.IsTrue(alertText.Contains("Product was added successfully!"));
            alert.Accept();
            Thread.Sleep(sleepTime);
            IWebElement shoppingCartIcon = driver.FindElement(By.Id("shoppingCartIcon"));
            shoppingCartIcon.Click();
            Thread.Sleep(sleepTime);
            IWebElement productInCart = driver.FindElement(By.Id("productName0"));
            IWebElement productQuant = driver.FindElement(By.Id("quantity0"));
            Assert.IsTrue(productInCart.Text.Equals("Milk chocolate"));
            Assert.IsTrue(productQuant.Text.Equals("2"));

        }

        [TestMethod]
        public void GUIaddProductToCartIncreaseDecreaseQuantity()
        {
            IWebElement AllProducts = driver.FindElement(By.Id("AllProductsLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime);
            IWebElement sale1 = driver.FindElement(By.Id("viewSale0"));
            sale1.Click();
            Thread.Sleep(sleepTime);
            IWebElement upQuan = driver.FindElement(By.Id("up-bar"));
            upQuan.Click();
            Thread.Sleep(sleepTime);
            upQuan.Click();
            Thread.Sleep(sleepTime);
            upQuan.Click();
            Thread.Sleep(sleepTime);
            upQuan.Click();
            Thread.Sleep(sleepTime);
            IWebElement downQuan = driver.FindElement(By.Id("down-bar"));
            downQuan.Click();
            Thread.Sleep(sleepTime);
            IWebElement submitViewInstantSale = driver.FindElement(By.Id("submit"));
            submitViewInstantSale.Click();
            Thread.Sleep(sleepTime);
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Assert.IsTrue(alertText.Contains("Product was added successfully!"));
            alert.Accept();
            Thread.Sleep(sleepTime);
            IWebElement shoppingCartIcon = driver.FindElement(By.Id("shoppingCartIcon"));
            shoppingCartIcon.Click();
            Thread.Sleep(sleepTime);
            IWebElement productInCart = driver.FindElement(By.Id("productName0"));
            IWebElement productQuant = driver.FindElement(By.Id("quantity0"));
            Assert.IsTrue(productInCart.Text.Equals("Milk chocolate"));
            Assert.IsTrue(productQuant.Text.Equals("4"));
        }

        [TestMethod]
        public void NextTest()
        {
            Console.WriteLine("Next method");
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Close();
            Console.WriteLine("Closed the browser");
        }
    }
}
