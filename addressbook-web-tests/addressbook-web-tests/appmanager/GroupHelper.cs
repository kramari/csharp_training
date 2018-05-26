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

        private List<GroupData> groupCache = null;
        
        public List<GroupData> GetGroupList()
        {
            if(groupCache == null)
            {
                //создаем кеш
                groupCache = new List<GroupData>();
                manager.Navigator.GoToGroupsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {
                    GroupData group = new GroupData(element.Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    };
                   
                    groupCache.Add(group);
                }
            }

            //выводим новый кеш, на случай, если кто-то проводил модификацию
            return new List<GroupData>(groupCache);
            
            /*
             * кусок кода, до того как мы стали использовать кеш
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
            return groups;*/

        }

        //для хеширования
        public int GetGroupCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
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

        // для изменения, в случае использования бд
        public GroupHelper Modify(GroupData oldGroups, GroupData newData)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(oldGroups.Id);
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

        //метод удаления по id
        public GroupHelper Remove(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();

            SelectGroup(group.Id);
            RemoverGroup();
            ReturnToGroupsPage();
            return this;
        }

        public bool GroupIsExists()
        {
            return IsElementPresent(By.XPath("(//input[@name='selected[]'])"));
        }

        public void GroupNotExists()
        {
            manager.Navigator.GoToGroupsPage();

            if (GroupIsExists() == false)
            {
                GroupData group = new GroupData("papapm");
                group.Header = "tadam";
                group.Footer = "bum";

                Create(group);
            }
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
            //чистим старый кеш
            groupCache = null;
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

        //показать группы (для варианта с бд)
        public GroupHelper SelectGroup(String id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='"+id+"'])")).Click();
            return this;
        }

        //удаление группы
        public GroupHelper RemoverGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            //чистим старый кеш
            groupCache = null;
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
            //чистим старый кеш
            groupCache = null;
            return this;
        }
    }
}
