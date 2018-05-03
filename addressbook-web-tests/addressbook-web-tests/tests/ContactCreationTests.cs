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
      
        [Test]
        public void ContactCreationTest()
        {
            // app.Contact.CreationNewContact();

            ContactData contact = new ContactData("Hohok", "Pum")
            {
                Middlename = "",
                Nickname = "",
                Title = "",
                Company = "",
                Address = "",
                Home = "",
                Email = ""
            };

            //проверяем количесиво уже созданных контактов
            List<ContactData> oldContacts = app.Contact.GetContacList();

            app.Contact.Create(contact);

            //хеширование
            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

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
