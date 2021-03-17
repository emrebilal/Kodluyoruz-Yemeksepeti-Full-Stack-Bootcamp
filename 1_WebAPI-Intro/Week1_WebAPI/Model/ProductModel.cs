using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week1_WebAPI.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public decimal Price { get; set; }
    }
}
