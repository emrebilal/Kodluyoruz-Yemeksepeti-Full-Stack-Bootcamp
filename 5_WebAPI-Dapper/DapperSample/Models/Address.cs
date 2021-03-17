using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSample.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string AddressInfo { get; set; }
    }
}
