using EFCoreGenericRepositoryPattern.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllWithInstructor();
        Task<Course> GetCourseById(int id);
        Task<IEnumerable<Course>> GetCoursesByInstructorId(int instructorId);
        Task<Course> AddCourse(Course course);
        Task UpdateCourse(Course courseToBeUpdate, Course course);
        Task DeleteCourse(Course course);
    }
}
