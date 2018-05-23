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
        public static IEnumerable<GroupData> RandomGetDataProvider()
        {
            List<GroupData> group = new List<GroupData>();

            //сколько сгененировать наборов тестовых данных
            for (int i = 0; i < 5; i++)
            {
                group.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
                
            }
            return group;
        }

        [Test, TestCaseSource("RandomGetDataProvider")]
        public void GroupCreationTest(GroupData group)
        {
            /*GroupData group = new GroupData("dhth");
            group.Header = "ntth";
            group.Footer = "cgnfg";*/

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

        /*[Test]
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
        }*/

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
