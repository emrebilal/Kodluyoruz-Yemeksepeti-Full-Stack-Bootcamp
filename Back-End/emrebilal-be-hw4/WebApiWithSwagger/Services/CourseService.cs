using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithSwagger.Data.Context;
using WebApiWithSwagger.Models;
using WebApiWithSwagger.Services.Interfaces;

namespace WebApiWithSwagger.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApiContext _context;
        public CourseService(ApiContext context)
        {
            _context = context;
        }

        public Course Add(Course course)
        {
            var addedCourse =_context.Add(course);
            _context.SaveChanges();
            course.Id = addedCourse.Entity.Id;

            return course;
        }

        public void Delete(Course course)
        {
            _context.Remove(course);
            _context.SaveChanges();
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public Course GetById(int id)
        {
            return _context.Courses.SingleOrDefault(c => c.Id == id);
        }

        public void Update(Course course)
        {
            var courseToUpdate = GetById(course.Id);
            courseToUpdate.CourseName = course.CourseName;
            courseToUpdate.CategoryName = course.CategoryName;
            courseToUpdate.InstructorName = course.InstructorName;
            courseToUpdate.Price = course.Price;

            _context.Update(courseToUpdate);
            _context.SaveChanges();
        }
    }
}
