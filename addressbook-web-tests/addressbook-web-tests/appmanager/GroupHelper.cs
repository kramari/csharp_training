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
    public class GroupHelper : HelperBase
    {

        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        //методы для создания
        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitToGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }

        public List<GroupData> GetGroupList()
        {
            //готовим пустой список
            List<GroupData> groups = new List<GroupData>();

            //заполняем список данными
            manager.Navigator.GoToGroupsPage();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
            foreach(IWebElement element in elements)
            {
                GroupData group = new GroupData(element.Text);
                groups.Add(group);
            }

            //возвращаем
            return groups;

        }

        //методы для изменения
        public GroupHelper Modify(int p, GroupData newData)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(p);
            InitToGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this;
            
        }

        //методы для удаления
        public GroupHelper Remove(int p)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(p);
            RemoverGroup();
            ReturnToGroupsPage();
            return this;
        }

        //создание новой группы
        public GroupHelper InitToGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }
        
        //заполнение данных о новой группе
        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }
        
        //нажатие кнопки о создании новой группы
        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        //возврат на страницу с группами
        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        //показать группы
        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        //удаление группы
        public GroupHelper RemoverGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        //нажать на кнопку редактирования группы
        public GroupHelper InitToGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        //сохранеие внесенных изменений в группу
        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }
    }
}
