using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {

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
        
        public string Email { get; set; }

        public string Id { get; set; }

    }
}
