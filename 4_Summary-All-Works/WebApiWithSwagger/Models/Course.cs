using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiWithSwagger.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CategoryName { get; set; }
        public string InstructorName { get; set; }
        public decimal Price { get; set; }
    }
}
