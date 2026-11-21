using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System
{
    internal class Employee
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string HireDate { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Employee other)
                return this.ID == other.ID;

            return false;
        }

        public override int GetHashCode()
        {
            return ID == null ? 0 : ID.GetHashCode();
        }
    }
}
