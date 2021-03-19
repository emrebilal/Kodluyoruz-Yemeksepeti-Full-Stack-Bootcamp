using LoggingMiddleware.Data;
using LoggingMiddleware.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingMiddleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SampleData _sampleData;
        public ProductsController(SampleData sampleData)
        {
            _sampleData = sampleData;    
        }

        [HttpGet]
        public List<Product> GetAll()
        {
            return _sampleData.Products;
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            var data = _sampleData.Products.FirstOrDefault(c => c.Id == id);

            return data;
        }

        [HttpPost]
        public Product Post([FromBody] Product product)
        {
            _sampleData.Products.Add(product);

            return product;
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            for (var index = _sampleData.Products.Count - 1; index >= 0; index--)
            {
                if (_sampleData.Products[index].Id == id)
                {
                    _sampleData.Products.RemoveAt(index);
                }
            }

            return id;
        }
    }
}
