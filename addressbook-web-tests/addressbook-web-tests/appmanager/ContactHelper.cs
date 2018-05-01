using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        //методы для создания контакта
        public ContactHelper Create(ContactData contact)
        {
            FillContactForm(contact);
            SubmitContactCreation();
            return this;
        }

        //методы для изменения контакта
        public ContactHelper Modify(int v, ContactData newData)
        {
            //SelectContact(v);
            SubmitContactEdit(v);
            FillContactForm(newData);
            SudbmitContactUpdate();
            return this;
        }
      
        //методы для удаления контакта
        public ContactHelper Remove(int v)
        {
            SelectContact(v);
            RemoverContact();
            DeleteOk();
            return this;
        }
        
        //создание нового контакта
        public ContactHelper CreationNewContact()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        //заполнение данных о новом контакте
        public ContactHelper FillContactForm(ContactData contact)
        {
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.Firstname);
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.Lastname);
            driver.FindElement(By.Name("middlename")).Clear();
            driver.FindElement(By.Name("middlename")).SendKeys(contact.Middlename);
            driver.FindElement(By.Name("nickname")).Clear();
            driver.FindElement(By.Name("nickname")).SendKeys(contact.Nickname);
            driver.FindElement(By.Name("title")).Clear();
            driver.FindElement(By.Name("title")).SendKeys(contact.Title);
            driver.FindElement(By.Name("company")).Clear();
            driver.FindElement(By.Name("company")).SendKeys(contact.Company);
            driver.FindElement(By.Name("address")).Clear();
            driver.FindElement(By.Name("address")).SendKeys(contact.Address);
            driver.FindElement(By.Name("home")).Clear();
            driver.FindElement(By.Name("home")).SendKeys(contact.Home);
            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys(contact.Email);
            return this;
        }

        //сохранение создания нового контакта
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            return this;
        }

        //выбор контакта
        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        //открытие радактирования контакта
        public ContactHelper SubmitContactEdit(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])["+ index +"]")).Click();
            //driver.FindElement(By.CssSelector("(img[alt=\"Edit\])[4]")).Click();
            return this;
        }

        //сохранение изменения контакта
        public ContactHelper SudbmitContactUpdate()
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            return this;
        }

        //удаление выбранного контакта
        public ContactHelper RemoverContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        //согласие с удалением выбранного контакта
        public ContactHelper DeleteOk()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }
    }
}
