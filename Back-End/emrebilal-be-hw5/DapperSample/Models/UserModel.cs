using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSample.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public List<Contact> Contacts { get; set; }
    }
}
