using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestDemo
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", "chromedriver.exe");
            IWebDriver driver = new ChromeDriver(); 
            driver.Navigate().GoToUrl("https://www.goibibo.com/");
        }
    }
}