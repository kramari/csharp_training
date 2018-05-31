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
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager)
        {
        }
               
        public List<ProjectData> GetProjectList()
        {
            //готовим пустой список
            List<ProjectData> projects = new List<ProjectData>();

            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr.row-1"));

                //.FindElements(By.TagName("td")));
            foreach (IWebElement element in elements.Skip(1))
            {
                IList<IWebElement> items = element.FindElements(By.CssSelector("td"));
                projects.Add(new ProjectData()
                {
                    Name = items[0].Text
                });
            }

            return projects;
        }

        public ProjectHelper CreateProject(ProjectData project)
        {
            OpenManagerProjects();
            CreateNewProject();
            FillProjectForm(project);
            SubmitCreationProject();
            return this;
        }

        public ProjectHelper Remove(int v)
        {
            OpenManagerProjects();
            OpenProject();
            SubmitDeleteProject();
            return this;
        }

        public ProjectHelper SubmitDeleteProject()
        {
            driver.FindElement(By.XPath("//input[@value='Delete Project']")).Click();
            return this;
        }

        public ProjectHelper OpenProject()
        {
            driver.FindElement(By.CssSelector(@"a[href*='manage_proj_edit_page.php?project_id=1']")).Click();
            return this;
        }

        public ProjectHelper SubmitCreationProject()
        {            
            driver.FindElement(By.XPath("//input[@value='Add Project']")).Click();
            return this;            
        }

        //заполнение формы
        public ProjectHelper FillProjectForm(ProjectData project)
        {
            Type(By.Name("name"), project.Name);
            Type(By.Name("description"), project.Description);
            return this;
        }

        //нажимаем на кнопку создания нового проекта
        public ProjectHelper CreateNewProject()
        {
            driver.FindElement(By.XPath("//input[@value='Create New Project']")).Click();
            return this;
        }

        //открываем страницу с менеджером проектов
        public ProjectHelper OpenManagerProjects()
        {
            driver.FindElement(By.LinkText("Manage")).Click();
            //System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Manage Projects")).Click();
            //System.Threading.Thread.Sleep(1000);
            return this;
        }
    }
}
