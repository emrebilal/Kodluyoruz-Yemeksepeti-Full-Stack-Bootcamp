using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithSwagger.Models;

namespace WebApiWithSwagger.Services.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAll();
        Course GetById(int id);
        Course Add(Course course);
        void Update(Course course);
        void Delete(Course course);
    }
}
