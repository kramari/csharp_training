using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {

        [Test]
        public void ContactRemovalTest()
        {
            app.Contact.ContacNotExists();

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRemove = oldContacts[0]; //сохраняем этот объект

            app.Contact.Remove(toBeRemove);

            //хеширование
            Assert.AreEqual(oldContacts.Count - 1, app.Contact.GetContactCount());

            app.Navigator.OpenHomePage();

            List<ContactData> newContacts = app.Contact.GetContacList();
            ContactData toBeRemoved = oldContacts[0];
            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
