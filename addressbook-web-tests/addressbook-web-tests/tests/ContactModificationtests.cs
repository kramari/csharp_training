using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {

        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("Ell", "Doomn");
            newData.Middlename = "";
            newData.Nickname = "";
            newData.Title = "";
            newData.Company = "";
            newData.Address = "";
            newData.Home = "";
            newData.Email = "";

            app.Contact.Modify(1, newData);
        }
    }

}
