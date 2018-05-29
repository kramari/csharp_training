using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_test_autoit
{
    public class GroupHelper : HelperBase
    {
        protected static string GROUPWINTITLE = "Group editor";

        public GroupHelper(ApplicationManager manager) : base(manager) { }

        public List<GroupData> GetGrupList()
        {
            List<GroupData> list = new List<GroupData>();
            OpenGroupsDialogue();
            string count = aux.ControlTreeView(
                GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                "GetItemCout", "#0", "");
            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(
                    GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                    "GetText", "#0|#"+1, "");
                list.Add(new GroupData()
                {
                    Name = item
                });
            }
            ClouseGroupsDialogue();
            return list;
        }

        public void Add(GroupData newGroup)
        {
            OpenGroupsDialogue();
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            aux.Send(newGroup.Name);
            aux.Send("{ENTER}");
            ClouseGroupsDialogue();
        }

        //закрыти окна диалога
        private void ClouseGroupsDialogue()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d54");
        }

        //метод открытия окна
        private void OpenGroupsDialogue()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d512");
            aux.WinWait(GROUPWINTITLE);
        }
    }
}