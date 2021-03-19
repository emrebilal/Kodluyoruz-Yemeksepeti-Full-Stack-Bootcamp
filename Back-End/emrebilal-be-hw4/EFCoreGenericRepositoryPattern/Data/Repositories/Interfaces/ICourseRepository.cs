using EFCoreGenericRepositoryPattern.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Data.Repositories.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllWithInstructorAsync();
        Task<Course> GetWithInstructorByIdAsync(int id);
        Task<IEnumerable<Course>> GetAllWithInstructorByInstructorIdAsync(int instructorId);
    }
}
