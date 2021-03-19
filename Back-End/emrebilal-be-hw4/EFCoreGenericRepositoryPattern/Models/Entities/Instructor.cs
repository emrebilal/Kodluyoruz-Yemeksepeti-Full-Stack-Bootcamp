using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Models.Entities
{
    public class Instructor : IEntity
    {
        public Instructor()
        {
            Courses = new Collection<Course>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Course> Courses { get; set; }


    }
}
