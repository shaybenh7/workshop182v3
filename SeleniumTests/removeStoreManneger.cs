using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using wsep182.services;

namespace SeleniumTests
{
    [TestClass]
    public class removeStoreManneger
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

            //add manneger
            IWebElement addStoreBtn = driver.FindElement(By.Id("addStoreManager0"));
            addStoreBtn.Click();
            Thread.Sleep(sleepTime);
            IWebElement mannegerName = driver.FindElement(By.Id("new-manager-name"));
            mannegerName.SendKeys("admin");
            Thread.Sleep(sleepTime);

            IWebElement addManagerBtn = driver.FindElement(By.Id("Add-manager-Btn"));
            addManagerBtn.Click();
            Thread.Sleep(sleepTime);

            IAlert alert2 = driver.SwitchTo().Alert();
            alert2.Accept();
            Thread.Sleep(sleepTime);
        }
        [TestMethod]
        public void simpleRemoveMannegers()
        {
            Thread.Sleep(sleepTime);
            IWebElement removeManagerBtn = driver.FindElement(By.Id("removeStoreManager0"));
            removeManagerBtn.Click();
            Thread.Sleep(sleepTime);
            IWebElement mannegerName = driver.FindElement(By.Id("old-manager-name"));
            mannegerName.SendKeys("admin");
            Thread.Sleep(sleepTime);
            IWebElement removeBtn = driver.FindElement(By.Id("Remove-manager-Btn"));
            removeBtn.Click();
            Thread.Sleep(sleepTime);
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Assert.IsTrue(alertText.Contains("Manager removed successfuly"));
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
