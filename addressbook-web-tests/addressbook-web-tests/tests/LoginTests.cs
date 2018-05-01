using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            //готовим тестовую ситуацию (подготовка)
            app.Auth.Loguot();

            //действие
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);

            //проверка
            Assert.IsTrue(app.Auth.IsLoggedIn(account));

        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            //готовим тестовую ситуацию (подготовка)
            app.Auth.Loguot();

            //действие
            AccountData account = new AccountData("admin", "12345");
            app.Auth.Login(account);

            //проверка
            Assert.IsFalse(app.Auth.IsLoggedIn(account));

        }

    }
}
