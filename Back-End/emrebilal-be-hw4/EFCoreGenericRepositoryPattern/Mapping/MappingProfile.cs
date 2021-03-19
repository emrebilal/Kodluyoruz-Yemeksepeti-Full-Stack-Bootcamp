using AutoMapper;
using EFCoreGenericRepositoryPattern.Models.DTO;
using EFCoreGenericRepositoryPattern.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<Instructor, InstructorDTO>();

            CreateMap<CourseDTO, Course>();
            CreateMap<InstructorDTO, Instructor>();

            CreateMap<SaveCourseDTO, Course>();
            CreateMap<SaveInstructorDTO, Instructor>();
        }
    }
}
