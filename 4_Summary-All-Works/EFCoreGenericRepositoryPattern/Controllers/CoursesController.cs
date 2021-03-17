using AutoMapper;
using EFCoreGenericRepositoryPattern.Models.DTO;
using EFCoreGenericRepositoryPattern.Models.Entities;
using EFCoreGenericRepositoryPattern.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        public CoursesController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetAllCourses()
        {
            var courses = await _courseService.GetAllWithInstructor();
            var courseResources = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDTO>>(courses);

            return Ok(courseResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseById(id);
            if (course == null)
                return NotFound();

            var courseResource = _mapper.Map<Course, CourseDTO>(course);

            return Ok(courseResource);
        }

        [HttpPost]
        public async Task<ActionResult<CourseDTO>> AddCourse([FromBody] SaveCourseDTO saveCourseResource)
        {
            var courseToAdd = _mapper.Map<SaveCourseDTO, Course>(saveCourseResource);

            var newCourse = await _courseService.AddCourse(courseToAdd);
            var course = await _courseService.GetCourseById(newCourse.Id);

            var courseResource = _mapper.Map<Course, CourseDTO>(course);

            return Ok(courseResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDTO>> UpdateCourse(int id, [FromBody] SaveCourseDTO saveCourseResource)
        {
            var courseToUpdated = await _courseService.GetCourseById(id);
            if (courseToUpdated == null)
                return NotFound();

            var course = _mapper.Map<SaveCourseDTO, Course>(saveCourseResource);
            await _courseService.UpdateCourse(courseToUpdated, course);

            var updatedCourse = await _courseService.GetCourseById(id);
            var updatedCourseResource = _mapper.Map<Course, CourseDTO>(updatedCourse);

            return Ok(updatedCourseResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var courseToDeleted = await _courseService.GetCourseById(id);
            if (courseToDeleted == null)
                return NotFound();

            await _courseService.DeleteCourse(courseToDeleted);
            return NoContent();
        }
    }
}
