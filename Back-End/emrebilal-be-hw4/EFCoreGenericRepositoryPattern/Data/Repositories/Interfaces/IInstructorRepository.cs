using EFCoreGenericRepositoryPattern.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Data.Repositories.Interfaces
{
    public interface IInstructorRepository : IRepository<Instructor>
    {
        Task<IEnumerable<Instructor>> GetAllWithCoursesAsync();
        Task<Instructor> GetWithCoursesByIdAsync(int id);
    }
}
