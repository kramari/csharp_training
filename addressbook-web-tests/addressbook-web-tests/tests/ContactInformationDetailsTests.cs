using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WebAddressbookTests
{
    class ContactInformationDetailsTests : AuthTestBase
    {
        [Test]
        public void ContactInformationDetailsTest()
        {
            ContactData fromDetales = app.Contact.GetContactInformationFromDetails(0);
            ContactData fromForm = app.Contact.GetContactInformationFromEditForm(0);

            Assert.AreEqual(fromDetales, fromForm);
            Assert.AreEqual(fromDetales.Address, fromForm.Address);
            Assert.AreEqual(fromDetales.Home, fromForm.Home);
            Assert.AreEqual(fromDetales.Mobile, fromForm.Mobile);
            Assert.AreEqual(fromDetales.Work, fromForm.Work);
            Assert.AreEqual(fromDetales.Email, fromForm.Email);
            Assert.AreEqual(fromDetales.Email2, fromForm.Email2);
            Assert.AreEqual(fromDetales.Email3, fromForm.Email3);
        }
    }
}
