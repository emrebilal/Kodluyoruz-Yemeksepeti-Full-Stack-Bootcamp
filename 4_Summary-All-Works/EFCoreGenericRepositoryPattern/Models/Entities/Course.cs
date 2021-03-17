using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Models.Entities
{
    public class Course : IEntity
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

    }
}
