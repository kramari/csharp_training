using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {

        [Test]
        public void GroupRemovalTest()
        {
            //проверяем количесиво уже созданных групп
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Remove(0);

            //хеширование
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            //получаем количество групп после удаления
            List<GroupData> newGroups = app.Groups.GetGroupList();

            //сохраняем сравниваемую группу в переменную
            GroupData toBeRemoved = oldGroups[0];
            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            //нужно убедится, что идентификатор этого элемента не равен идентификатору удаленного элемента
           /* foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }*/
        }
    }
}
