using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WebAddressbookTests
{

    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {

        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("ttt")
            {
                Header = null,
                Footer = null
            };

            //проверяем количесиво уже созданных групп
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Modify(0, newData);

            //получаем количество групп после создания новой
            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();

            //проверяем, что после создания новой группы, их стало на 1 больше
            Assert.AreEqual(oldGroups, newGroups);

        }
    }
}
