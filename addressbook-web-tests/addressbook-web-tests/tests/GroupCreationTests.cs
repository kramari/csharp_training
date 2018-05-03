using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
       
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("dhth");
            group.Header = "ntth";
            group.Footer = "cgnfg";

            //проверяем количесиво уже созданных групп
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            //хеширование
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            //контейнер/коллекция
            //получаем количество групп после создания новой
            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            //упорядочиваем перед сравнением 
            oldGroups.Sort();
            newGroups.Sort();

            //проверяем, что после создания новой группы, их стало на 1 больше
            Assert.AreEqual(oldGroups, newGroups);
           
        }

        [Test]
        //создание пустой группы
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            //хеширование
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        //тест с недопустимым именем
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData("d'c");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            //хеширование
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
