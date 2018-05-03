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
            List<ContactData> oldContacts = app.Contact.GetContacList();
            ContactData oldData = oldContacts[0];

            app.Contact.Modify(1, newData);

            //получаем количество контактов после создания нового
            List<ContactData> newContacts = app.Contact.GetContacList();
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts[0].Lastname = newData.Lastname;
            oldContacts.Sort();
            newContacts.Sort();

            //проверяем, что после создания нового контакта, их стало на 1 больше
            Assert.AreEqual(oldContacts, newContacts);

            //находим тот элемент, у которого нужный идентификатор
            foreach (ContactData contract in newContacts)
            {
                if (contract.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Lastname, contract.Lastname);
                    Assert.AreEqual(newData.Firstname, contract.Firstname);                    
                }
            }
        }
    }

}
