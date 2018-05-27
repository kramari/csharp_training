using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            //выбираем группу
            GroupData group = GroupData.GetAll()[0];
            //запоминаем старый список контактов, который был уже в группе
            List<ContactData> oldList = group.GetContacts();
            //теперь проходимя по всем контактам и выбираем первый попавшийся кьнтакт, котрый и будем добавлять в группу
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            //выполняем действия
            app.Contact.AddContactToGroup(contact, group);

            //сравниваем
            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);

        }
    }
}
