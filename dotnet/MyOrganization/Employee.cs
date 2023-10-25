using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyOrganization
{
    internal class Employee
    {
        private Guid identifier;
        private Name name;

        public Employee(Guid identifier, Name name)
        {
            if (name == null)
                throw new Exception("name cannot be null");
            this.identifier = identifier;
            this.name = name;
        }

        public Guid GetIdentifier()
        {
            return identifier;
        }

        public Name GetName()
        {
            return name;
        }

        override public string ToString()
        {
            return name.ToString() + ": " + identifier;
        }
    }
}
