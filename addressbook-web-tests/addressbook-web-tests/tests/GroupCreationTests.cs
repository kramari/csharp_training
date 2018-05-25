using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using Newtonsoft.Json;

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

        //чтение из файл формата csv
        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            //создаем список
            List<GroupData> group = new List<GroupData>();

            //прочитать каждую строчку, строчку разделить на куски по какому-то символу
            //а куски будут использоваться в качетсве значений для объектов типа GroupData

            //file - класс для работы с файлами/ читаем их файл и записываем в массив
            string[] lines = File.ReadAllLines(@"group.csv");

            foreach (string l in lines)
            {
                //получаем набор кусочков
                string[] parts = l.Split(',');

                //создаем новый объект и добавляем его в список групп
                group.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }

            //возвращаем список
            return group;
        }

        //чтение из файл формата xml
        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            return (List<GroupData>) //приведение типа, мы должны явно указать, что знаем какого типа объект
                new XmlSerializer(typeof(List<GroupData>)) //читает данные типа List<GroupData>
                    .Deserialize(new StreamReader(@"group.xml")); //из указанного файла
        }

        //чтение из файл формата json
        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"group.json"));
        }

        [Test, TestCaseSource("GroupDataFromJsonFile")]
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
