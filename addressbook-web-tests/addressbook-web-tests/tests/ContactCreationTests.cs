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
    public class ContactCreationTests : ContactTestBase
    {

        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contact = new List<ContactData>();

            //сколько сгененировать наборов тестовых данных
            for (int i = 0; i < 5; i++)
            {
                contact.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30))
                {
                    Middlename = GenerateRandomString(30),
                    Nickname = GenerateRandomString(30),
                    Title = GenerateRandomString(30),
                    Company = GenerateRandomString(30),
                    Address = GenerateRandomString(100),
                    Home = GenerateRandomString(30),
                    Email = GenerateRandomString(30)
                });
            }
            return contact;
        }

        //чтение из файл формата xml
        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>) //приведение типа, мы должны явно указать, что знаем какого типа объект
                new XmlSerializer(typeof(List<ContactData>)) //читает данные типа List<GroupData>
                    .Deserialize(new StreamReader(@"contact.xml")); //из указанного файла
        }

        //чтение из файл формата json
        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contact.json"));
        }

        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactCreationTest(ContactData contact)
        {
            //проверяем количесиво уже созданных контактов
            List<ContactData> oldContacts = ContactData.GetAll();

            app.Contact.Create(contact);

            //хеширование
            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

            app.Navigator.OpenHomePage();

            //получаем количество контактов после создания нового
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Add(contact);
            //упорядочиваем перед сравнением 
            oldContacts.Sort();
            newContacts.Sort();
            //проверяем, что после создания нового контакта, их стало на 1 больше
            Assert.AreEqual(oldContacts, newContacts);
        }

       /* [Test]
        public void TestDBConnectivity()
        {

        }*/

    }
}
