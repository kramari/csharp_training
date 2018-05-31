using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;

namespace mantis_tests
{
    /// <summary>
    /// Сводное описание для AccountCreationTests
    /// </summary>
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [SetUp]
        public void setUpConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localFile);
            }

        }

        [Test]
        public void TestAccountRegestration()
        {
            AccountData account = new AccountData()
            {
                Name = "testuser",
                Passwd = "passwd",
                Email = "testuser@localhost.localdomain"
            };

            app.Regestration.Register(account);
        }

        [TearDown]
        public void restoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }
    }
}
