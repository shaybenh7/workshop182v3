using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using wsep182.services;

namespace SeleniumTests
{
    [TestClass]
    public class editProductInStore
    {
        [TestClass]
        public class AddProductToStore
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
                alert = driver.SwitchTo().Alert();
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

                 crateStoreBtn = driver.FindElement(By.Id("addProductInStore0"));
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
                Thread.Sleep(sleepTime);

                 alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                Assert.IsTrue(alertText.Contains("successfuly added"));
                alert.Accept();
                Thread.Sleep(sleepTime);

            }
            [TestMethod]
            public void editProduct()
            {
                IWebElement MystoreBtn = driver.FindElement(By.Id("MyStoresPublicLink"));
                MystoreBtn.Click();
                Thread.Sleep(sleepTime);
                IWebElement editProductInStoreBtn = driver.FindElement(By.Id("editProductInStore0"));
                editProductInStoreBtn.Click();
                Thread.Sleep(sleepTime);
                IWebElement pisId = driver.FindElement(By.Id("product-id2"));
                pisId.SendKeys("3");
                Thread.Sleep(sleepTime);
                IWebElement price = driver.FindElement(By.Id("product-price2"));
                price.SendKeys("12");
                Thread.Sleep(sleepTime);
                IWebElement amount = driver.FindElement(By.Id("product-amount2"));
                amount.SendKeys("12");
                Thread.Sleep(sleepTime);
                IWebElement editPro = driver.FindElement(By.Id("aviad-Edit-product"));
                editPro.Click();
                Thread.Sleep(sleepTime);
                
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                Assert.IsTrue(alertText.Contains("product edited successfuly"));
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
}
