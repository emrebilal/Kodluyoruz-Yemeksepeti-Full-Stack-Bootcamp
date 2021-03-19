using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithSwagger.Models;

namespace WebApiWithSwagger.Services.Interfaces
{
    public interface IInstructorService
    {
        IEnumerable<Instructor> GetAll();
        Instructor GetById(int id);
        Instructor Add(Instructor instructor);
        void Update(Instructor instructor);
        void Delete(Instructor instructor);
    }
}
