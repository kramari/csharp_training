using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class DellingContactOfGroupTests : AuthTestBase
    {
        [Test]
        public void TestDelContactOfGroup()
        {
            //выбираем группу
            GroupData group = GroupData.GetAll()[0];
            //запоминаем старый список контактов, который был уже в группе
            List<ContactData> oldList = group.GetContacts();
            //теперь проходимя по всем контактам и выбираем первый попавшийся кьнтакт, котрый и будем добавлять в группу
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            //выполняем действия
            app.Contact.DelContactOfGroup(contact, group);

             //сравниваем
            List<ContactData> newList = group.GetContacts();
            //newList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
