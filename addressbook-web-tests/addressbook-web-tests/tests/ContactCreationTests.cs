using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

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

            app.Contact.Create(contact);
        }

     }
}
