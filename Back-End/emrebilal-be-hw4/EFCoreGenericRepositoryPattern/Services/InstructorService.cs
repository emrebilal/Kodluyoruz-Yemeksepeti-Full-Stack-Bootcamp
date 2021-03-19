using EFCoreGenericRepositoryPattern.Data;
using EFCoreGenericRepositoryPattern.Models.Entities;
using EFCoreGenericRepositoryPattern.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InstructorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;          
        }

        public async Task<Instructor> AddInstructor(Instructor instructor)
        {
            await _unitOfWork.Instructors.AddAsync(instructor);
            await _unitOfWork.CommitAsync();
            return instructor;
        }

        public async Task DeleteInstructor(Instructor instructor)
        {
            _unitOfWork.Instructors.Delete(instructor);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Instructor>> GetAllInstructors()
        {
            return await _unitOfWork.Instructors.GetAllAsync();
        }

        public async Task<Instructor> GetInstructorById(int id)
        {
            return await _unitOfWork.Instructors.GetByIdAsync(id);
        }

        public async Task UpdateInstructor(Instructor instructorToBeUpdated, Instructor instructor)
        {
            instructorToBeUpdated.FirstName = instructor.FirstName;
            instructorToBeUpdated.LastName = instructor.LastName;
            instructorToBeUpdated.Email = instructor.Email;

            await _unitOfWork.CommitAsync();
        }
    }
}
