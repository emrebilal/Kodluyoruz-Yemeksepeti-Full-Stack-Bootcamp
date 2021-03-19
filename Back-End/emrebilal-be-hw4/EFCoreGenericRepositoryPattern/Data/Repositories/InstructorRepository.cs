using EFCoreGenericRepositoryPattern.Data.Repositories.Interfaces;
using EFCoreGenericRepositoryPattern.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Data.Repositories
{
    public class InstructorRepository : Repository<Instructor>, IInstructorRepository
    {
        private ApiContext ApiContext { get { return dbContext as ApiContext; } }
        public InstructorRepository(ApiContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Instructor>> GetAllWithCoursesAsync()
        {
            return await dbContext.Instructors
                .Include(i => i.Courses)
                .ToListAsync();
        }

        public Task<Instructor> GetWithCoursesByIdAsync(int id)
        {
            return dbContext.Instructors
                .Include(i => i.Courses)
                .SingleOrDefaultAsync(i => i.Id == id);
        }
    }
}
