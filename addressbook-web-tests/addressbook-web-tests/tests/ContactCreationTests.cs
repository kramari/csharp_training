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
            app.Contact.CreationNewContact();

            ContactData contact = new ContactData("Gabbi", "Timi");
            contact.Middlename = "";
            contact.Nickname = "";
            contact.Title = "";
            contact.Company = "";
            contact.Address = "";
            contact.Home = "";
            contact.Email = "";

            //проверяем количесиво уже созданных контактов
            List<ContactData> oldContacts = app.Contact.GetContacList();

            app.Contact.Create(contact);

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
