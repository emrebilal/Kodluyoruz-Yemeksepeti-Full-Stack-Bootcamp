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
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IEnumerable<Course> GetAll()
        {
            return _courseService.GetAll();
        }

        [HttpGet("{id}", Name = "GetCourse")]
        public IActionResult Get(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Course course)
        {
            var createdCourse = _courseService.Add(course);

            return CreatedAtRoute("GetCourse", new { id = createdCourse.Id }, createdCourse);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest();
            }

            if (_courseService.GetById(id) == null)
            {
                return NotFound();
            }

            course.Id = id;
            _courseService.Update(course);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            _courseService.Delete(course);

            return NoContent();
        }
    }
}
