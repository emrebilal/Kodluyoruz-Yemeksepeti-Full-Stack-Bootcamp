using EFCoreGenericRepositoryPattern.Data.Repositories.Interfaces;
using EFCoreGenericRepositoryPattern.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Data.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private ApiContext ApiContext { get { return dbContext as ApiContext; } }
        public CourseRepository(ApiContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Course>> GetAllWithInstructorAsync()
        {
            return await dbContext.Courses
                .Include(c => c.Instructor)
                .ToListAsync();
        }

        public async Task<Course> GetWithInstructorByIdAsync(int id)
        {
            return await dbContext.Courses
                .Include(c => c.Instructor)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Course>> GetAllWithInstructorByInstructorIdAsync(int instructorId)
        {
            return await dbContext.Courses
                .Include(c => c.Instructor)
                .Where(c => c.InstructorId == instructorId)
                .ToListAsync();
        }
    }
}
