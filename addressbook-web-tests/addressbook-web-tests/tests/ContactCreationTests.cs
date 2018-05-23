using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
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

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
            // app.Contact.CreationNewContact();

           /* ContactData contact = new ContactData("Hohok", "Pum")
            {
                Middlename = "",
                Nickname = "",
                Title = "",
                Company = "",
                Address = "",
                Home = "",
                Email = ""
            };*/

            //проверяем количесиво уже созданных контактов
            List<ContactData> oldContacts = app.Contact.GetContacList();

            app.Contact.Create(contact);

            //хеширование
            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

            app.Navigator.OpenHomePage();

            //получаем количество контактов после создания нового
            List<ContactData> newContacts = app.Contact.GetContacList();
            oldContacts.Add(contact);
            //упорядочиваем перед сравнением 
            oldContacts.Sort();
            newContacts.Sort();
            //проверяем, что после создания нового контакта, их стало на 1 больше
            Assert.AreEqual(oldContacts, newContacts);
        }

     }
}
