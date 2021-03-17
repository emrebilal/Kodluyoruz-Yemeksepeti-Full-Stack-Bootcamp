using EFCoreGenericRepositoryPattern.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }
        IInstructorRepository Instructors { get; }
        Task<int> CommitAsync();
    }
}
