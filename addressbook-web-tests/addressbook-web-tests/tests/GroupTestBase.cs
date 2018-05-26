using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class GroupTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareGroupsUI_DB() //метод - универсальная проверка, которая будет выполнятся после каждого метода
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                //получаем списки
                List<GroupData> fromUI = app.Groups.GetGroupList();
                List<GroupData> fromDB = GroupData.GetAll();
                //сортируем
                fromUI.Sort();
                fromDB.Sort();
                //сравниваем
                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}
