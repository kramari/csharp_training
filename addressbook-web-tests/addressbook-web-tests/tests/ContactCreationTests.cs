using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
      
        [Test]
        public void ContactCreationTest()
        {
            //app.Contact.CreationNewContact();
            ContactData contact = new ContactData("Timi", "Gabbi");
            contact.Middlename = "";
            contact.Nickname = "";
            contact.Title = "";
            contact.Company = "";
            contact.Address = "";
            contact.Home = "";
            contact.Email = "";
        }

     }
}
