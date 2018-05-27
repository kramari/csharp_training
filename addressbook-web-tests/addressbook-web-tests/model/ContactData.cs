using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhone;
        private string allEmail;

        public ContactData()
        {
        }

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public bool Equals(ContactData other)  //метод Equals - стандартный метод
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return (Firstname == other.Firstname)
                 && (Lastname == other.Lastname);
        }

        //этот метод предназначен для оптимизации сравнения, используется с Equals
        public override int GetHashCode()
        {
            //если оптимизация не нужна, то просто пишем:
            return 0;

            //если хотим, чтобы работала:
            //return Firstname.GetHashCode();
           // return Lastname.GetHashCode();
        }

        public override string ToString()
        {
            return "name =" + Lastname + Firstname + "\n address = " + Address + "\n home = " + Home 
                + "\n mobile = " + Mobile + "\n work = " + Work + "\n email = " + Email + "\n email2 = " +Email2 + "\n email3 = " + Email3;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (Lastname.CompareTo(other.Lastname) == 0)
            {
                return Firstname.CompareTo(other.Lastname);
            }
            return Lastname.CompareTo(other.Lastname);
        }

        [Column(Name = "firstname"), NotNull]
        public string Firstname { get; set; }

        [Column(Name = "lastname"), NotNull]
        public string Lastname { get; set; }

        [Column(Name = "middlename"), NotNull]
        public string Middlename { get; set; }

        [Column(Name = "nickname"), NotNull]
        public string Nickname { get; set; }

        [Column(Name = "title"), NotNull]
        public string Title { get; set; }

        [Column(Name = "company"), NotNull]
        public string Company { get; set; }

        [Column(Name = "address"), NotNull]
        public string Address { get; set; }

        [Column(Name = "home"), NotNull]
        public string Home { get; set; }

        [Column(Name = "mobile"), NotNull]
        public string Mobile { get; set; }

        [Column(Name = "work"), NotNull]
        public string Work { get; set; }

        public string AllPhone
        {
            get {
                if(allPhone != null)
                {
                    return allPhone;
                }
                else {
                    //метод Trim удаляет лишние пробелы в строчке
                    return (CleanUpPhone(Home) + CleanUpPhone(Mobile) + CleanUpPhone(Work)).Trim();
                }
            }
            set {
                allPhone = value;
            }
        }

        private string CleanUpPhone(string phone)
        {
            if(phone == null || phone == "")
            {
                return "";
            }
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "")+ "\r\n";
        }

        [Column(Name = "email"), NotNull]
        public string Email { get; set; }

        [Column(Name = "email2"), NotNull]
        public string Email2 { get; set; }

        [Column(Name = "email3"), NotNull]
        public string Email3 { get; set; }

        public string AllEmail
        {
            get
            {
                if (allEmail != null)
                {
                    return allEmail;
                }
                else
                {
                    //метод Trim удаляет лишние пробелы в строчке
                    return (CleanUpEmail(Email) + CleanUpEmail(Email2) + CleanUpEmail(Email3)).Trim();
                }
            }
            set
            {
                allEmail = value;
            }
        }

        private string CleanUpEmail(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return email.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
        }

        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public static List<ContactData> GetAll() //метод чтения из бд
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        where c.Deprecated == "0000-00-00 00:00:00"
                        select c).ToList();
            }
        }

    }
}
