using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Test_login
{
    
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            var options = new OpenQA.Selenium.Chrome.ChromeOptions();
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

           

            driver = new OpenQA.Selenium.Chrome.ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://passport.yandex.ua/auth?origin=home_desktop_ua&retpath=https%3A%2F%2Fmail.yandex.ua%2F&backpath=https%3A%2F%2Fyandex.ua");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            // 1) Enter login on the site

            IWebElement login = driver.FindElement(By.Id("passp-field-login"));
            string login_text = "ortanov909500@yandex.ua";
            login.SendKeys(login_text);

            // 2) Click the "login" button

            IWebElement click_login = driver.FindElement(By.XPath("//button[@class='Button2 Button2_size_l Button2_view_action Button2_width_max Button2_type_submit']"));
            click_login.Click();

            // 3) wait for id password

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("passp-field-passwd")));

            // 4) Enter the password

            IWebElement password = driver.FindElement(By.Id("passp-field-passwd"));
            string password_text = "653849653320ds";
            password.SendKeys(password_text);

            // 5) Repeat click_login

            IWebElement click_login2 = driver.FindElement(By.XPath("//button[@class='Button2 Button2_size_l Button2_view_action Button2_width_max Button2_type_submit']"));
            click_login2.Click();

            // 6) wait 5 seconds for loading page

            Thread.Sleep(5000);
            driver.Navigate().GoToUrl("https://passport.yandex.ua/profile");

            // 7) wait for load ClassName perfonal info

            var wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait2.Until(d => d.FindElement(By.ClassName("personal-info-login")));

            // 8) compare actual and expected login

            string real_login = driver.FindElement(By.ClassName("personal-info-login")).Text;
            string login_expected = "ortanov909500";
            Assert.AreEqual(login_expected, real_login, "the actual login and the expected login do not match ");
        }

        [TearDown]
        public void TearDown()
        {
            //close the browser automation after 10 seconds
            Thread.Sleep(10000);
            driver.Close();
        }
    }
}