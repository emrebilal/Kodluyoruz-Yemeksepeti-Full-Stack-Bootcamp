using EFCoreGenericRepositoryPattern.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Services.Interfaces
{
    public interface IInstructorService
    {
        Task<IEnumerable<Instructor>> GetAllInstructors();
        Task<Instructor> GetInstructorById(int id);
        Task<Instructor> AddInstructor(Instructor instructor);
        Task UpdateInstructor(Instructor instructorToBeUpdated, Instructor instructor);
        Task DeleteInstructor(Instructor instructor);
    }
}
