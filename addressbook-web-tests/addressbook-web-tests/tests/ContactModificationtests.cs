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
            app.Contact.ContacNotExists();

            ContactData newData = new ContactData("Rerv", "Tyhd")
            {
                Middlename = null,
                Nickname = null,
                Title = null,
                Company = null,
                Address = null,
                Home = null,
                Email = null
            };
            

            //проверяем количесиво уже созданных контактов
            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData oldData = oldContacts[0];

            app.Contact.Modify(oldData, newData);

            //получаем количество контактов после создания нового
            List<ContactData> newContacts = ContactData.GetAll();
            oldData.Lastname = newData.Lastname;
            oldData.Firstname = newData.Firstname;
            
            oldContacts.Sort();
            newContacts.Sort();

            //проверяем, что после создания нового контакта, их стало на 1 больше
            Assert.AreEqual(oldContacts, newContacts);

        }
    }

}
