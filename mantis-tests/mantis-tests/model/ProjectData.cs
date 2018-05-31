using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public bool Equals(ProjectData other)  //метод Equals - стандартный метод
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name;
        }

        public int CompareTo(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return "Project name = " + Name;
        }

        //этот метод предназначен для оптимизации сравнения, используется с Equals
        public override int GetHashCode()
        {
            //если оптимизация не нужна, то просто пишем:
            //return 0;

            //если хотим, чтобы работала:
            return Name.GetHashCode();
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
