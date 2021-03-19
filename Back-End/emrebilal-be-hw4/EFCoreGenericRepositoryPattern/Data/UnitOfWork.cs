using EFCoreGenericRepositoryPattern.Data.Repositories;
using EFCoreGenericRepositoryPattern.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiContext _context;
        private CourseRepository _courseRepository;
        private InstructorRepository _instructorRepository;

        public UnitOfWork(ApiContext context)
        {
            _context = context;
        }
        public ICourseRepository Courses => _courseRepository = _courseRepository ?? new CourseRepository(_context);

        public IInstructorRepository Instructors => _instructorRepository = _instructorRepository ?? new InstructorRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
