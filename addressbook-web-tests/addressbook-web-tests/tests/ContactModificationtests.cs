using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {

        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("Holms", "Vatson");
            newData.Middlename = null;
            newData.Nickname = null;
            newData.Title = null;
            newData.Company = null;
            newData.Address = null;
            newData.Home = null;
            newData.Email = null;

            //проверяем количесиво уже созданных контактов
            List<ContactData> oldContacts = app.Contact.GetContacList();

            app.Contact.Modify(1, newData);

            //получаем количество контактов после создания нового
            List<ContactData> newContacts = app.Contact.GetContacList();
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts[0].Lastname = newData.Lastname;
            oldContacts.Sort();
            newContacts.Sort();

            //проверяем, что после создания нового контакта, их стало на 1 больше
            Assert.AreEqual(oldContacts, newContacts);
        }
    }

}
