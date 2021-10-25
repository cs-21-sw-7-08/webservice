using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wasp.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public User(string name = null, string email = null)
        {
            Name = name;
            Email = email;
        }
    }
}
