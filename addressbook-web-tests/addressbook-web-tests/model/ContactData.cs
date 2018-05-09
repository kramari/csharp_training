using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhone;
        private string allEmail;

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
            //return "name = " + Firstname;
            return "name=" + Lastname;
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

        public string Firstname { get; set; }

        public string Lastname { get; set; }
    
        public string Middlename { get; set; }
        
        public string Nickname { get; set; }
       
        public string Title { get; set; }
        
        public string Company { get; set; }
       
        public string Address { get; set; }
        
        public string Home { get; set; }

        public string Mobile { get; set; }

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

        public string Email { get; set; }

        public string Email2 { get; set; }

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

        public string Id { get; set; }

    }
}
