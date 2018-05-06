using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {

        [Test]
        public void ContactRemovalTest()
        {
            List<ContactData> oldContacts = app.Contact.GetContacList();

            app.Contact.Remove(0);

            //хеширование
            Assert.AreEqual(oldContacts.Count - 1, app.Contact.GetContactCount());

            app.Navigator.OpenHomePage();

            List<ContactData> newContacts = app.Contact.GetContacList();
            ContactData toBeRemoved = oldContacts[0];
            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);           
        }
    }
}
