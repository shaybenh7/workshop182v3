using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace SeleniumTests
{
    [TestClass]
    public class viewAllProducts
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
        }

        [TestMethod]
        public void GUIviewAllProducts()
        {
            IWebElement AllProducts = driver.FindElement(By.Id("AllProductsLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime);
            IWebElement product0 = driver.FindElement(By.Id("productName0"));
            Assert.IsTrue(product0.Text.Contains("Milk chocolate"));
            IWebElement product1 = driver.FindElement(By.Id("productName1"));
            Assert.IsTrue(product1.Text.Contains("Dark chocolate"));
        }
        [TestMethod]
        public void GUIviewAllProductsAndGetInTheProduct()
        {
            IWebElement AllProducts = driver.FindElement(By.Id("AllProductsLink"));
            AllProducts.Click();
            Thread.Sleep(sleepTime);
            IWebElement product0 = driver.FindElement(By.Id("viewSale0"));
            product0.Click();
            Thread.Sleep(sleepTime);
            IWebElement productName = driver.FindElement(By.Id("productName"));
            Assert.IsTrue(productName.Text.Contains("Milk chocolate"));
            IWebElement storeName = driver.FindElement(By.Id("storeName"));
            Assert.IsTrue(storeName.Text.Contains("Maria&Netta"));
            IWebElement price = driver.FindElement(By.Id("salePrice"));
            Assert.IsTrue(price.Text.Contains("3.2"));
        }
        [TestCleanup]
        public void CleanUp()
        {
            driver.Close();
            Console.WriteLine("Closed the browser");
        }
    }
}
