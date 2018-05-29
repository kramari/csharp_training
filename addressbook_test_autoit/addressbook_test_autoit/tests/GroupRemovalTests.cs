using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_test_autoit
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> oldListGroups = app.Groups.GetGrupList();
            GroupData removeGroup = new GroupData()
            {
                Name = "test"
            };

            int index = 1;

            app.Groups.Del(index);

            List<GroupData> newListGroups = app.Groups.GetGrupList();

            oldListGroups.Sort();
            newListGroups.Sort();

            Assert.AreEqual(oldListGroups, newListGroups);
        }
    }
}
