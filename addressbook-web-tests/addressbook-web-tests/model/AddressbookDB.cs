using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;

namespace WebAddressbookTests
{
    //класс, которы представляет собой соединение с бд
    public class AddressBookDB : LinqToDB.Data.DataConnection
    {
        //какой конекшн нужно использовать
        public AddressBookDB() : base("AddressBook") { }

        //метод для каждой таблиц, который возвращет таблицу данных
        public ITable<GroupData> Groups { get { return GetTable<GroupData>(); } }

        public ITable<ContactData> Contacts { get { return GetTable<ContactData>(); } }

        public ITable<GroupContactRelation> GCR { get { return GetTable<GroupContactRelation>(); } }
    }
}
