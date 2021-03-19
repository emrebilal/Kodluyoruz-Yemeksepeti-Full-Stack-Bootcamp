using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week2_WebAPI.Data.Abstract;
using Week2_WebAPI.Data.Context;
using Week2_WebAPI.Mapping;
using Week2_WebAPI.Models;

namespace Week2_WebAPI.Data
{
    public class InMemoryCarRepository : ICarRepository
    {
        private readonly CarsAPIContext _context;
        public InMemoryCarRepository(CarsAPIContext context)
        {
            _context = context;
        }

        public Car Add(Car car)
        {
            var addedCar = _context.Add(car);
            _context.SaveChanges();
            car.Id = addedCar.Entity.Id;

            return car;
        }

        public void Delete(Car car)
        {
            _context.Remove(car);
            _context.SaveChanges();
        }

        public IEnumerable<Car> GetAll()
        {
            return _context.Cars.ToList();
        }

        public IEnumerable<CarDTO> GetUI()
        {
            return _context.Cars.ToList().ToViewModel();
        }

        public Car GetById(int id)
        {
            return _context.Cars.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Car car)
        {
            var carToUpdate = GetById(car.Id);
            carToUpdate.BrandName = car.BrandName;
            carToUpdate.Color = car.Color;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.ModelYear = car.ModelYear;

            _context.Update(carToUpdate);
            _context.SaveChanges();
        }
    }
}
