using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Models.DTO
{
    public class SaveCourseDTO
    {
        public string CourseName { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int InstructorId { get; set; }
    }
}
