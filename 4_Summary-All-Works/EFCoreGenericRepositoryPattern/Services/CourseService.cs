using EFCoreGenericRepositoryPattern.Data;
using EFCoreGenericRepositoryPattern.Models.Entities;
using EFCoreGenericRepositoryPattern.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Course> AddCourse(Course course)
        {
            await _unitOfWork.Courses.AddAsync(course);
            await _unitOfWork.CommitAsync();
            return course;
        }

        public async Task DeleteCourse(Course course)
        {
            _unitOfWork.Courses.Delete(course);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Course>> GetAllWithInstructor()
        {
            return await _unitOfWork.Courses.GetAllWithInstructorAsync();
        }

        public async Task<Course> GetCourseById(int id)
        {
            return await _unitOfWork.Courses.GetWithInstructorByIdAsync(id);
        }

        public async Task<IEnumerable<Course>> GetCoursesByInstructorId(int instructorId)
        {
            return await _unitOfWork.Courses.GetAllWithInstructorByInstructorIdAsync(instructorId);
        }

        public async Task UpdateCourse(Course courseToBeUpdate, Course course)
        {
            courseToBeUpdate.CourseName = course.CourseName;
            courseToBeUpdate.CategoryName = course.CategoryName;
            courseToBeUpdate.Price = course.Price;
            courseToBeUpdate.InstructorId = course.InstructorId;

            await _unitOfWork.CommitAsync();
        }
    }
}
