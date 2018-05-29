using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace addressbook_test_autoit
{
    [TestFixture]
    public class GroupCrationTests : TestBase
    {
        [Test]
        public void TestMethod1()
        {
            List<GroupData> oldGroups = app.Groups.GetGrupList();

            GroupData newGroup = new GroupData()
            {
                Name = "test"
            };

            app.Groups.Add(newGroup);

            List<GroupData> newGroups = app.Groups.GetGrupList();
            oldGroups.Add(newGroup);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);

        }
    }
}
