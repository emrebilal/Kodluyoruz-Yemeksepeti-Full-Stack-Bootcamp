using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Week2_WebAPI.Validation;

namespace Week2_WebAPI.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Brand cannot be blank!")]
        public string BrandName { get; set; }
        [Required(ErrorMessage = "Color cannot be blank!")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Year cannot be blank!")]
        [MaxYear(ErrorMessage = "Invalid year!")]
        public int ModelYear { get; set; }
        [Required(ErrorMessage = "Price cannot be blank!")]
        [MinPrice(ErrorMessage = "Price must be equal or greater than 50!")]
        public decimal DailyPrice { get; set; }

    }
}
