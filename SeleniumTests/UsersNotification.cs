using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using wsep182.services;

namespace SeleniumTests
{
    [TestClass]
    public class UsersNotification
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


            login = driver.FindElement(By.Id("LoginLink"));
            login.Click();
            Thread.Sleep(sleepTime);
            IWebElement userName2 = driver.FindElement(By.Id("username"));
            userName2.SendKeys("zahiSimpleRegister");
            Thread.Sleep(sleepTime);
            password = driver.FindElement(By.Id("password"));
            password.SendKeys("123456");
            Thread.Sleep(sleepTime);
            btnLogin = driver.FindElement(By.Id("btnLogin"));
            btnLogin.Click();
            Thread.Sleep(sleepTime*2);

            //add store
            IWebElement AllProducts = driver.FindElement(By.Id("MyStoresPublicLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime*3);
            IWebElement addStore = driver.FindElement(By.Id("storeName"));
            addStore.SendKeys("abowStore");
            Thread.Sleep(sleepTime);
            IWebElement createStoreButton12 = driver.FindElement(By.Id("createStoreButton12"));
            createStoreButton12.Click();
            Thread.Sleep(sleepTime * 3);
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
            Thread.Sleep(sleepTime*3);

            IAlert alert2 = driver.SwitchTo().Alert();
            alert2.Accept();
            Thread.Sleep(sleepTime);

            //add Sale
            IWebElement addSaleToStoreBtn = driver.FindElement(By.Id("addSaleToStore0"));
            addSaleToStoreBtn.Click();
            Thread.Sleep(sleepTime);
            IWebElement amount = driver.FindElement(By.Id("product-amount-in-sale2"));
            amount.SendKeys("10");
            Thread.Sleep(sleepTime);
            IWebElement date = driver.FindElement(By.Id("product-due-date2"));
            date.SendKeys("20/08/2018");
            Thread.Sleep(sleepTime);
            IWebElement addSale = driver.FindElement(By.Id("AddSaleBtn"));
            addSale.Click();
            Thread.Sleep(sleepTime*3);
            alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(sleepTime);

            //log-out
            logout = driver.FindElement(By.Id("LogoutLink"));
            logout.Click();
            Thread.Sleep(sleepTime*3);

            

        }

        [TestMethod]
        public void simpleGetBuyAlert()
        {
            IWebElement AllProducts = driver.FindElement(By.Id("AllProductsLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime * 2);
            IWebElement sale1 = driver.FindElement(By.Id("viewSale0"));
            sale1.Click();
            Thread.Sleep(sleepTime * 2);
            IWebElement submitViewInstantSale = driver.FindElement(By.Id("submit"));
            submitViewInstantSale.Click();
            Thread.Sleep(sleepTime * 2);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(sleepTime);
            IWebElement shoppingCartIcon = driver.FindElement(By.Id("shoppingCartIcon"));
            shoppingCartIcon.Click();
            Thread.Sleep(sleepTime * 2);
            IWebElement country = driver.FindElement(By.Id("country"));
            country.SendKeys("Israel");
            Thread.Sleep(sleepTime);
            IWebElement address = driver.FindElement(By.Id("address"));
            address.SendKeys("haifa 4");
            Thread.Sleep(sleepTime);
            IWebElement creditCard = driver.FindElement(By.Id("creditCard"));
            creditCard.SendKeys("1234");
            Thread.Sleep(sleepTime);
            IWebElement checkout = driver.FindElement(By.Id("aviad-Checkout"));
            checkout.Click();
            Thread.Sleep(sleepTime * 2);
            checkout = driver.FindElement(By.Id("purchase_btn"));
            checkout.Click();
            Thread.Sleep(sleepTime * 3);
            alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(sleepTime);

            IWebElement login = driver.FindElement(By.Id("LoginLink"));
            login.Click();
            Thread.Sleep(sleepTime);
            IWebElement userName2 = driver.FindElement(By.Id("username"));
            userName2.SendKeys("zahiSimpleRegister");
            Thread.Sleep(sleepTime);
            IWebElement password = driver.FindElement(By.Id("password"));
            password.SendKeys("123456");
            Thread.Sleep(sleepTime);
            IWebElement btnLogin = driver.FindElement(By.Id("btnLogin"));
            btnLogin.Click();
            Thread.Sleep(sleepTime * 2);

            alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Assert.IsTrue(alertText.Contains(""));

        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Close();
            Console.WriteLine("Closed the browser");
        }
    }
}
