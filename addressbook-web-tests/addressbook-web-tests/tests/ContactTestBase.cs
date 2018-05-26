using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareContactsUI_DB() //метод - универсальная проверка, которая будет выполнятся после каждого метода
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                //получаем списки
                List<ContactData> fromUI = app.Contact.GetContacList();
                List<ContactData> fromDB = ContactData.GetAll();
                //сортируем
                fromUI.Sort();
                fromDB.Sort();
                //сравниваем
                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}
