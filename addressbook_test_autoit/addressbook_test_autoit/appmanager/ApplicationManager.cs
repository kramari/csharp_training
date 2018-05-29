using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoItX3Lib;

namespace addressbook_test_autoit
{
    public class ApplicationManager
    {
        public static string WINTITLE = "Free Address Book"; //вынесли параметр название окна - отдельной переменной, т.к. будет часто встречаться
        
        //добавляем сслыку
        private AutoItX3 aux;
        
        private GroupHelper groupHelper;

        public ApplicationManager()
        {
            aux = new AutoItX3();
            //запуск приложения 
            aux.Run(@"C:\Users\Administrator\source\repos\AddressBook.exe", "", aux.SW_SHOW); //первый параметр - путь к прложению, остальные можно проигнорировать
            aux.WinWait(WINTITLE); //ожидаем открытие окна
            aux.WinWaitActive(WINTITLE);
            aux.WinWaitActive(WINTITLE);

            groupHelper = new GroupHelper(this);
        }

        public void Stop()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d510"); //1й - окно в котором нужно нажать кнопку, 2 - текст кнопки,3 - иденификатор кнопки (ее локатор)
        }

        public AutoItX3 Aux
        {
            get
            {
                return aux;
            }
        }

        public GroupHelper Groups
        {
            get
            {
                return groupHelper;
            }
        }
    }

    
}
