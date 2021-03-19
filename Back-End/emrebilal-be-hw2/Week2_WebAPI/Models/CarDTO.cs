using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week2_WebAPI.Models
{
    public class CarDTO
    {
        public string BrandName { get; set; }
        public int ModelYear { get; set; }
        public string Color { get; set; }
        public decimal DailyPrice { get; set; }
    }
}
