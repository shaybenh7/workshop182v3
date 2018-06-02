using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    [TestClass]
    public class addPolicy
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

            Thread.Sleep(sleepTime);

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
        public void TestMethod1()
        {
            IWebElement login = driver.FindElement(By.Id("LoginLink"));
            login.Click();
            Thread.Sleep(sleepTime);
            IWebElement userName = driver.FindElement(By.Id("username"));
            userName.SendKeys("zahi");
            Thread.Sleep(sleepTime);
            IWebElement password = driver.FindElement(By.Id("password"));
            password.SendKeys("123");
            Thread.Sleep(sleepTime);
            IWebElement btnLogin = driver.FindElement(By.Id("btnLogin"));
            btnLogin.Click();
            Thread.Sleep(sleepTime);


            IWebElement MystoreBtn = driver.FindElement(By.Id("MyStoresPublicLink"));
            MystoreBtn.Click();
            Thread.Sleep(sleepTime*3);
            IWebElement viewAddPolicy = driver.FindElement(By.Id("viewAddPolicy0"));
            viewAddPolicy.Click();
            Thread.Sleep(sleepTime);
            IWebElement minPolicy = driver.FindElement(By.Id("minPolicy"));
            minPolicy.SendKeys("2");
            Thread.Sleep(sleepTime);
            IWebElement maxPolicy = driver.FindElement(By.Id("maxPolicy"));
            maxPolicy.SendKeys("5");
            Thread.Sleep(sleepTime);
            IWebElement PolicyChange = driver.FindElement(By.Id("PolicyChange"));
            PolicyChange.SendKeys("1");
            Thread.Sleep(sleepTime*3);
            viewAddPolicy = driver.FindElement(By.Id("addPolicy33"));
            viewAddPolicy.Click();
            Thread.Sleep(sleepTime);
            

            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Assert.IsTrue(alertText.Contains("Policy added successfully"));
            alert.Accept();

            Thread.Sleep(sleepTime*2);

            MystoreBtn = driver.FindElement(By.Id("AllProductsLink"));
            MystoreBtn.Click();
            Thread.Sleep(sleepTime * 3);
            MystoreBtn = driver.FindElement(By.Id("viewSale0"));
            MystoreBtn.Click();
            Thread.Sleep(sleepTime * 3);
            IWebElement policyOfInstantSale = driver.FindElement(By.Id("policyOfInstantSale"));
            Assert.IsTrue(policyOfInstantSale.Text.Contains("Minimum amount per order: 2"));

        }
        [TestCleanup]
        public void CleanUp()
        {
            driver.Close();
            Console.WriteLine("Closed the browser");
        }
    }
}
