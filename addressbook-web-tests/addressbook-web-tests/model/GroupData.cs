using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable <GroupData>, IComparable<GroupData>
    {
        public GroupData()
        {

        }

        public GroupData(string name)
        {
            Name = name;
        }

            //свойства
        [Column(Name = "group_name"), NotNull]
        public string Name { get; set; }

        [Column(Name = "group_header"), NotNull]
        public string Header { get; set; }

        [Column(Name = "group_footer"), NotNull]
        public string Footer { get; set; }

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }
        
        public bool Equals(GroupData other)  //метод Equals - стандартный метод
        {
            if(Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if(Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name;
        }

        //этот метод предназначен для оптимизации сравнения, используется с Equals
        public override int GetHashCode()
        {
            //если оптимизация не нужна, то просто пишем:
            //return 0;

            //если хотим, чтобы работала:
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name = " + Name + "\n heder = " + Header + "\n footer = " + Footer;
        }

        public int CompareTo(GroupData other)
        {
            if(Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        public static List<GroupData> GetAll() //метод чтения из бд
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();
            }
        }

        public List<ContactData> GetContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        from gcr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id)
                        select c).Distinct().ToList();
            }
        }
    }
}
