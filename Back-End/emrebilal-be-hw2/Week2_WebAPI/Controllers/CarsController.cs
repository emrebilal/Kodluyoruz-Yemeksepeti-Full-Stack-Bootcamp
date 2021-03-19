using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week2_WebAPI.Data.Abstract;
using Week2_WebAPI.Models;

namespace Week2_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;
        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet("/rent-a-car")]
        public IEnumerable<CarDTO> GetUI()
        {
            return _carRepository.GetUI();
        }

        [HttpGet("/api/cars")]
        public IEnumerable<Car> GetAll()
        {
            return _carRepository.GetAll();
        }

        [HttpGet("/api/cars/{id}", Name = "GetCar")]
        public IActionResult GetCar(int id)
        {
            var car = _carRepository.GetById(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost("/api/cars")]
        public IActionResult Post([FromBody] Car carData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_carRepository.GetById(carData.Id) != null)
                return Conflict("Id already exists!");
            
            var createdCar = _carRepository.Add(carData);

            return CreatedAtRoute("GetCar", new {id = createdCar.Id}, createdCar);
        }

        [HttpPut("/api/cars/{id}")]
        public IActionResult UpdateCar(int id, [FromBody] Car car)
        {
            if(car == null)
            {
                return BadRequest();
            }

            if(_carRepository.GetById(id) == null)
            {
                return NotFound();
            }

            car.Id = id;
            _carRepository.Update(car);

            return NoContent();

        }

        [HttpDelete("/api/cars/{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = _carRepository.GetById(id);
            if (car == null)
            {
                return NotFound();
            }

            _carRepository.Delete(car);

            return NoContent();
        }
    }
}
