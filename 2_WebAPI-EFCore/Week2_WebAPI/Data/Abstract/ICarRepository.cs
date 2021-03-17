using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week2_WebAPI.Models;

namespace Week2_WebAPI.Data.Abstract
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAll();
        IEnumerable<CarDTO> GetUI();
        Car GetById(int id);
        Car Add(Car car);
        void Update(Car car);
        void Delete(Car car);
    }
}
