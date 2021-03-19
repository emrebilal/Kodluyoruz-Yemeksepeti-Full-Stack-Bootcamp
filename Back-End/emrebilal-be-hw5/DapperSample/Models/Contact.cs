using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSample.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ContactName { get; set; }
        public Address Address { get; set; }
    }
}
