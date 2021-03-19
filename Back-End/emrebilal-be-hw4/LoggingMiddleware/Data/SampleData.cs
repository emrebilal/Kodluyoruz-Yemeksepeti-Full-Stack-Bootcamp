using LoggingMiddleware.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingMiddleware.Data
{
    public class SampleData
    {
        public List<Product> Products = new List<Product>();

        public SampleData()
        {
            for (int i = 1; i <= 10; i++)
            {
                Products.Add(new Product { Id = i, ProductName = "Product_" + i, Price = 1000 + (i * 100) });
            }
        }
    }
}
