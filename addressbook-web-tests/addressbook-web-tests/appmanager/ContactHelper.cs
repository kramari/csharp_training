using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }
                
        private List<ContactData> contactCache = null;

        public List<ContactData> GetContacList()
        {
            if(contactCache == null)
            {
                contactCache = new List<ContactData>();

                //готовим пустой список
                //List<ContactData> contacts = new List<ContactData>();

                //заполняем список данными
                manager.Navigator.OpenHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));

                foreach (IWebElement element in elements)
                {
                    IList < IWebElement > contact = element.FindElements(By.CssSelector("td"));
                    contactCache.Add(new ContactData(contact[1].Text, contact[2].Text));
                }
            }
            //возвращаем
            return contactCache;
        }

        //для хеширования
        public int GetContactCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }

        //методы для создания контакта
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.OpenHomePage();
            CreationNewContact();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.OpenHomePage();
            return this;
        }

        //методы для изменения контакта
        public ContactHelper Modify(int v, ContactData newData)
        {
            manager.Navigator.OpenHomePage();
            SubmitContactEdit(v);
            FillContactForm(newData);
            SudbmitContactUpdate();
            manager.Navigator.OpenHomePage();
            return this;
        }

        //методы для изменения контакта (случай с бд)
        public ContactHelper Modify(ContactData oldData, ContactData newData)
        {
            manager.Navigator.OpenHomePage();
            SubmitContactEdit(oldData.Id);
            FillContactForm(newData);
            SudbmitContactUpdate();
            manager.Navigator.OpenHomePage();
            return this;
        }

        //методы для удаления контакта
        public ContactHelper Remove(int v)
        {
            manager.Navigator.OpenHomePage();
            SelectContact(v);
            RemoverContact();
            DeleteOk();
            manager.Navigator.OpenHomePage();
            return this;
        }

        //методы для удаления контакта (случай с бд)
        public ContactHelper Remove(ContactData contact)
        {
            manager.Navigator.OpenHomePage();
            SelectContact(contact.Id);
            RemoverContact();
            DeleteOk();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public bool ContacIsExists()
        {
            return IsElementPresent(By.XPath("(//input[@name='selected[]'])"));
        }

        public void ContacNotExists()
        {
            manager.Navigator.OpenHomePage();

            if (ContacIsExists() == false)
            {
                ContactData contact = new ContactData("kukuev", "nab")
                {
                    Middlename = "",
                    Nickname = "",
                    Title = "",
                    Company = "",
                    Address = "",
                    Home = "",
                    Email = ""
                };

                Create(contact);
            }
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

        //выбор контакта по id
        public ContactHelper SelectContact(String contacId)
        {
            driver.FindElement(By.Id(contacId)).Click();
            return this;
        }

        //открытие радактирования контакта
        public ContactHelper SubmitContactEdit(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])["+ index +"]")).Click();
            return this;
        }

        public ContactHelper SubmitContactEdit(string id)
        {
            //driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + id + "]")).Click();
            driver.FindElement(By.Id(id)).Click();
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
            contactCache = null;
            return this;
        }

        //согласие с удалением выбранного контакта
        public ContactHelper DeleteOk()
        {
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        //метод для получения информации из таблицы с контактами
        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhone = cells[5].Text;
            string allEmail = cells[4].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhone = allPhone,
                AllEmail = allEmail
            };
        }

        //метод получения данных со страницы редактирования контакта
        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(0);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                Home = homePhone,
                Mobile = mobilePhone,
                Work = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };
        }

        public void InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index].
                FindElements(By.TagName("td"))[7].
                FindElement(By.TagName("a")).Click();
        }

        //метод получения данных со страницы с детялями о контакте
        public ContactData GetContactInformationFromDetails(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactDetails(0);

            string[] firstLastName = driver.FindElement(By.Id("content"))
                .FindElement(By.TagName("b"))
                .Text.Split(' ');

            string firstName = firstLastName[0];
            string lastName = firstLastName[1];

            string address = driver.FindElement(By.Id("content")).FindElement(By.TagName("br")).Text;

            string homePhone = driver.FindElement(By.Id("content")).FindElement(By.TagName("br")).Text;
            string mobilePhone = driver.FindElement(By.Id("content")).FindElement(By.TagName("br")).Text;
            string workPhone = driver.FindElement(By.Id("content")).FindElement(By.TagName("br")).Text;

            string email = driver.FindElement(By.Id("content")).FindElement(By.TagName("br")).Text;
            string email2 = driver.FindElement(By.Id("content")).FindElement(By.TagName("br")).Text;
            string email3 = driver.FindElement(By.Id("content")).FindElement(By.TagName("br")).Text;
            

            return new ContactData(firstName, lastName)
            {
                Address = address,
                Home = homePhone,
                Mobile = mobilePhone,
                Work = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };

        }

        public void InitContactDetails(int index)
        {
            driver.FindElements(By.Name("entry"))[index].
                FindElements(By.TagName("td"))[6].
                FindElement(By.TagName("a")).Click();
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }

        //метод для добавления контакта в группу
        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        //вспомогтельные методы для AddContactToGroup()

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        //метод для удаления контакта из группы
        public void DelContactOfGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            SelectGroupFilter(group.Name);
            SelectContact(contact.Id);
            RemoveFromGroup();
            //manager.Navigator.OpenHomePage();
            /*new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);*/
        }

        //вспомогтельные мтоды для DelContactOfGroup
        private void SelectGroupFilter(string name)
        {
            //new SelectElement(driver.FindElement(By.XPath("//input[@value='"+ id + "'")));
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(".2");
        }

        private void RemoveFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }
    }
}
