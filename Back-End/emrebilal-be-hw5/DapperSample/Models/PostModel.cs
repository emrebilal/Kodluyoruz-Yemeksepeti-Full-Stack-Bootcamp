using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSample.Models
{
    public class PostModel
    {
        public UserModel User { get; set; }
        public Contact Contact{ get; set; }
    }
}
