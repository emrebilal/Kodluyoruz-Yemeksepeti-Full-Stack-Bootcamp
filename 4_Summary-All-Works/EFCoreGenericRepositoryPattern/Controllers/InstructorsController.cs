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
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;
        public InstructorsController(IInstructorService instructorService, IMapper mapper)
        {
            _instructorService = instructorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorDTO>>> GetAllInstructors()
        {
            var instructors = await _instructorService.GetAllInstructors();
            var instructorResources = _mapper.Map<IEnumerable<Instructor>, IEnumerable<InstructorDTO>>(instructors);

            return Ok(instructorResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorDTO>> GetInstructorById(int id)
        {
            var instructor = await _instructorService.GetInstructorById(id);
            if (instructor == null)
                return NotFound();

            var instructorResource = _mapper.Map<Instructor, InstructorDTO>(instructor);

            return Ok(instructorResource);
        }

        [HttpPost]
        public async Task<ActionResult<InstructorDTO>> AddInstructor([FromBody] SaveInstructorDTO saveInstructorResource)
        {
            var instructorToAdd = _mapper.Map<SaveInstructorDTO, Instructor>(saveInstructorResource);

            var newInstructor = await _instructorService.AddInstructor(instructorToAdd);
            var instructor = await _instructorService.GetInstructorById(newInstructor.Id);

            var instuctorResource = _mapper.Map<Instructor, InstructorDTO>(instructor);

            return Ok(instuctorResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InstructorDTO>> UpdateInstructor(int id, [FromBody] SaveInstructorDTO saveInstructorResource)
        {
            var instructorToUpdated = await _instructorService.GetInstructorById(id);
            if (instructorToUpdated == null)
                return NotFound();

            var instructor = _mapper.Map<SaveInstructorDTO, Instructor>(saveInstructorResource);
            await _instructorService.UpdateInstructor(instructorToUpdated, instructor);

            var updatedInstructor = await _instructorService.GetInstructorById(id);
            var updatedInstructorResource = _mapper.Map<Instructor, InstructorDTO>(updatedInstructor);

            return Ok(updatedInstructorResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var instructorToDeleted = await _instructorService.GetInstructorById(id);
            if (instructorToDeleted == null)
                return NotFound();

            await _instructorService.DeleteInstructor(instructorToDeleted);
            return NoContent();
        }
    }
}
