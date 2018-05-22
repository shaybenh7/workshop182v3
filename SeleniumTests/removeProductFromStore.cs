using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    [TestClass]
    public class removeProductFromStore
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

            }

            [TestMethod]
            public void simpleRemoveProduct()
            {

                IWebElement removeProduct = driver.FindElement(By.Id("removeProductFromStore0"));
                removeProduct.Click();
                Thread.Sleep(sleepTime);
                IWebElement productName = driver.FindElement(By.Id("product-id3"));
                productName.SendKeys("3");
                Thread.Sleep(sleepTime);
                IWebElement removeProductBtn = driver.FindElement(By.Id("aviad-Remove-product"));
                removeProductBtn.Click();
                Thread.Sleep(sleepTime);
                
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                Assert.IsTrue(alertText.Contains("product removed successfuly"));
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
