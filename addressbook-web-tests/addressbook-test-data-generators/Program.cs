using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WebAddressbookTests;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string typeData = args[0]; //тип данных добавился 
            int count = Convert.ToInt32(args[1]);//в 0м параметре передаем количество тестовых даных, которые нужно сгенерить
            //StreamWriter writer = new StreamWriter(args[2]); //передаем параметр по которому определяется в какой файл записывать, а не само имя файла
            string filename = args[2];
            string format = args[3]; //третий параметр - какой формат нам нужен    

            if(typeData == "group")
            {
                //формируем список
                List<GroupData> groups = new List<GroupData>();
                //параметры получили, начинаем генерировать
                for (int i = 0; i < count; i++)
                {
                    //создаем объекты и добавляем их в список
                    groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(10),
                        Footer = TestBase.GenerateRandomString(10)
                    });

                }

                if(format == "excel")
                {
                    writeGroupToExcelFile(groups, filename);
                }
                else
                {
                    StreamWriter writer = new StreamWriter(filename); //передаем параметр по которому определяется в какой файл записывать, а не само имя файла
                    if (format == "csv")
                    {
                        writeGroupToCsvFile(groups, writer);
                    }
                    else if (format == "xml")
                    {
                        writeGroupToXmlFile(groups, writer);
                    }
                    else if (format == "json")
                    {
                        writeGroupToJsonFile(groups, writer);
                    }
                    else
                    {
                        System.Console.Out.Write("Unrecognized format " + format);
                    }
                    writer.Close();
                }                
            }
            else if(typeData == "contact")
            {
                //формируем список
                List<ContactData> contacts = new List<ContactData>();
                StreamWriter writer = new StreamWriter(args[2]);
                //параметры получили, начинаем генерировать
                for (int i = 0; i < count; i++)
                {
                    //создаем объекты и добавляем их в список
                    contacts.Add(new ContactData(TestBase.GenerateRandomString(10), TestBase.GenerateRandomString(10))
                    {
                        Address = TestBase.GenerateRandomString(10),
                        Home = TestBase.GenerateRandomString(10),
                        Mobile = TestBase.GenerateRandomString(10),
                        Work = TestBase.GenerateRandomString(10),
                        Email = TestBase.GenerateRandomString(10),
                        Email2 = TestBase.GenerateRandomString(10),
                        Email3 = TestBase.GenerateRandomString(10)
                    });

                }

               if (format == "xml")
                {
                    writeContactToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    writeContactToJsonFile(contacts, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognized format " + format);
                }
                writer.Close();
            }
        }

        ////запись в файл формата excel
        static void writeGroupToExcelFile(List<GroupData> groups, string filename)
        {
            //запуск excel
            Excel.Application app = new Excel.Application();
            app.Visible = true;//дает возможность наблюдать то, что происходит в окне приложения 
            Excel.Workbook wb = app.Workbooks.Add();//создаем новый документ Workbook
            Excel.Worksheet sheed = wb.ActiveSheet;//страничка файла
                                                   //sheed.Cells[1, 1] = "test";//вписываем на ту страницу текст,

            //запись в excel
            int row = 1;
            foreach (GroupData group in groups)
            {
                sheed.Cells[row, 1] = group.Name;
                sheed.Cells[row, 2] = group.Header;
                sheed.Cells[row, 3] = group.Footer;
                row++;
            }

            //сохранить, но для начала удаляем такой же файл в этой директори, если он уже там есть
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);

            wb.Close();
            app.Visible = false;
            app.Quit();//останавливаем фоновый процесс
        }

        //запись в файл формата csv
        static void writeGroupToCsvFile(List<GroupData> groups, StreamWriter writer)//в качестве параметра элементы списка GroupData
        {
            foreach(GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0}, ${1}, ${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        //запись в файл формата xml для групп
        static void writeGroupToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        //запись в файл формата xml для контактов
        static void writeContactToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        //запись в файл формата json для групп
        static void writeGroupToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));//Formatting.Indented для красивого форматирования файла
        }

        //запись в файл формата json для контактов
        static void writeContactToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));//Formatting.Indented для красивого форматирования файла
        }
    }
}
