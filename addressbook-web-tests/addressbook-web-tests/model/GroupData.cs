using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupData : IEquatable <GroupData>, IComparable<GroupData>
    {
        
        public GroupData(string name)
        {
            Name = name;
        }

        /*public GroupData(string name, string header, string footer)
        {
            this.name = name;
            this.header = header;
            this.footer = footer;
        }*/

            //свойства
        public string Name { get; set; }
        
        public string Header { get; set; }
       
        public string Footer { get; set; }

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
            return "name = " + Name;
        }

        public int CompareTo(GroupData other)
        {
            if(Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }
    }
}
