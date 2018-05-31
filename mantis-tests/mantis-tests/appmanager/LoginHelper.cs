using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
  
        public LoginHelper(ApplicationManager manager) :base(manager)
        {
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }

                Loguot();
            }
            Type(By.Name("username"), account.Name);
            Type(By.Name("password"), account.Passwd);
            driver.FindElement(By.CssSelector("input.button")).Click();
        }

        //выход
        public void Loguot()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.LinkText("Logout")).Click();
            }
            
        }

        //проверяем, что вод выполнен
        public bool IsLoggedIn()
        {
            return IsElementPresent(By.Name("logout"));
        }

        //проверяем, что вход выполнен определенным пользователем 
        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggetUserName() == account.Name;
                
        }

        private string GetLoggetUserName()
        {
            string text = driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text;
            //Substring - режет строки
            //отреем от строки 1 и 2 последних символа
            return text.Substring(1, text.Length - 2);
        }
    }
}
