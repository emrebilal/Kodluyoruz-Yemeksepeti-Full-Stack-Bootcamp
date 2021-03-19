using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithSwagger.Models;
using WebApiWithSwagger.Services.Interfaces;

namespace WebApiWithSwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        public IEnumerable<Instructor> GetAll()
        {
            return _instructorService.GetAll();
        }

        [HttpGet("{id}", Name = "GetInstructor")]
        public IActionResult Get(int id)
        {
            var instructor = _instructorService.GetById(id);
            if (instructor == null)
            {
                return NotFound();
            }

            return Ok(instructor);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Instructor instructor)
        {
            var createdInstructor = _instructorService.Add(instructor);

            return CreatedAtRoute("GetInstructor", new { id = createdInstructor.Id }, createdInstructor);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Instructor instructor)
        {
            if (instructor == null)
            {
                return BadRequest();
            }

            if (_instructorService.GetById(id) == null)
            {
                return NotFound();
            }

            instructor.Id = id;
            _instructorService.Update(instructor);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var instructor = _instructorService.GetById(id);
            if (instructor == null)
            {
                return NotFound();
            }

            _instructorService.Delete(instructor);

            return NoContent();
        }
    }
}
