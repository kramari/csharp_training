using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {

        [Test]
        public void GroupRemovalTest()
        {
            app.Groups.GroupNotExists();
                        
            //проверяем количесиво уже созданных групп
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeRemove = oldGroups[0]; //сохраняем этот объект

            app.Groups.Remove(toBeRemove);

            //хеширование
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            //получаем количество групп после удаления
            List<GroupData> newGroups = GroupData.GetAll();

            //сохраняем сравниваемую группу в переменную
            GroupData toBeRemoved = oldGroups[0];
            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            //нужно убедится, что идентификатор этого элемента не равен идентификатору удаленного элемента
           foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
