using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovaTests : AuthTestBase
    {
        [Test]
        public void ProjectRemovaTest()
        {
            List<ProjectData> oldProjects = app.Project.GetProjectList();

            app.Project.Remove(0);

            List<ProjectData> newProjects = app.Project.GetProjectList();
            oldProjects.RemoveAt(0);
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
