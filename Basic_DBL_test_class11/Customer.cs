using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_DBL_test_class11
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public Customer() { }
        public Customer(int id, string name, string email, bool isAdmin = false)
        {
            Id = id;
            Name = name;
            Email = email;
            IsAdmin = isAdmin;
        }
    }
}
