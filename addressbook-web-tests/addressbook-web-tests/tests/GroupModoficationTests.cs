using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WebAddressbookTests
{

    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {

        [Test]
        public void GroupModificationTest()
        {
            app.Groups.GroupNotExists();

            GroupData newData = new GroupData("ttt")
            {
                Header = null,
                Footer = null
            };

            //проверяем количесиво уже созданных групп
            List<GroupData> oldGroups = GroupData.GetAll();

            //запоминаем информацию для дальнейшего сравнения
            GroupData oldData = oldGroups[0];

            app.Groups.Modify(oldData, newData);

            //хеширование
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            //получаем количество групп после создания новой
            List<GroupData> newGroups = GroupData.GetAll();
            oldData.Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();

            //проверяем, что после создания новой группы, их стало на 1 больше
            Assert.AreEqual(oldGroups, newGroups);

            //находим тот элемент, у которого нужный идентификатор
            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }

        }
    }
}
