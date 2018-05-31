using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]
        public void ProjectCreationTest()
        {
            ProjectData project = new ProjectData()
            {
                Name = "test3"
            };

            List<ProjectData> oldProjects = app.Project.GetProjectList();

            foreach (ProjectData old in oldProjects)
            {
                if (old.Name == project.Name)
                {
                    project.Name = project.Name + "changed";
                    return;
                }
            }

            app.Project.CreateProject(project);

            List<ProjectData> newProjects = app.Project.GetProjectList();
            oldProjects.Add(project);
            //упорядочиваем перед сравнением 
            oldProjects.Sort();
            newProjects.Sort();
            //проверяем, что после создания нового контакта, их стало на 1 больше
            Assert.AreEqual(oldProjects, newProjects);


        }
    }
}
