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

        public List<ContactData> GetContacList()
        {
            //готовим пустой список
            List<ContactData> contacts = new List<ContactData>();

            //заполняем список данными
            manager.Navigator.OpenHomePage();
            ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));

            foreach (IWebElement element in elements)
            {
                IList<IWebElement> contact = element.FindElements(By.CssSelector("td"));
                contacts.Add(new ContactData(contact[1].Text, contact[2].Text));
            }

            //возвращаем
            return contacts;
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
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.Home);
            Type(By.Name("email"), contact.Email);
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
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        //открытие радактирования контакта
        public ContactHelper SubmitContactEdit(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])["+ (index + 1) +"]")).Click();
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
