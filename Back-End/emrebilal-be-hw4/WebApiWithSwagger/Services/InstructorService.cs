using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithSwagger.Data.Context;
using WebApiWithSwagger.Models;
using WebApiWithSwagger.Services.Interfaces;

namespace WebApiWithSwagger.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly ApiContext _context;
        public InstructorService(ApiContext context)
        {
            _context = context;
        }

        public Instructor Add(Instructor instructor)
        {
            var addedInstructor = _context.Add(instructor);
            _context.SaveChanges();
            instructor.Id = addedInstructor.Entity.Id;

            return instructor;
        }

        public void Delete(Instructor instructor)
        {
            _context.Remove(instructor);
            _context.SaveChanges();
        }

        public IEnumerable<Instructor> GetAll()
        {
            return _context.Instructors.ToList();
        }

        public Instructor GetById(int id)
        {
            return _context.Instructors.SingleOrDefault(i => i.Id == id);
        }

        public void Update(Instructor instructor)
        {
            var instructorToUpdate = GetById(instructor.Id);
            instructorToUpdate.FirstName = instructor.FirstName;
            instructorToUpdate.LastName = instructor.LastName;
            instructorToUpdate.Email = instructor.Email;
            instructorToUpdate.NumberOfCourse = instructor.NumberOfCourse;

            _context.Update(instructorToUpdate);
            _context.SaveChanges();
        }
    }
}
