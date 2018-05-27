using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {
        public static bool PERFORM_LONG_UI_CHECKS = false;
        protected ApplicationManager app;

        [SetUp]
        public void SetupApplicationManager()
        {
           app = ApplicationManager.GetInstance();
        }

        //генератор случайных чисел, теперь один генератор чисел на весь проект
        public static Random rnd = new Random();

        //генератор случайных строк
        public static string GenerateRandomString (int max)
        {

            //Convert - преобразует
            int l = Convert.ToInt32(rnd.NextDouble() * max);

            //генерируем случайые символ, а их них строку
            StringBuilder builder = new StringBuilder();
            //цикл, который генерирует символы
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65)));
            }
            //извлекаем из билдер получившуюся строку
            return builder.ToString();
        }

    }
}
