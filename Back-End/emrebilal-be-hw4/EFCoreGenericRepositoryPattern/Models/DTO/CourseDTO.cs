using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Models.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public InstructorDTO Instructor { get; set; }
    }
}
