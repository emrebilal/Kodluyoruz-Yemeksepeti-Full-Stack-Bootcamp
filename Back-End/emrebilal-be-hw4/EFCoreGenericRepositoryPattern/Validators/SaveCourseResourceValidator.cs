using EFCoreGenericRepositoryPattern.Models.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Validators
{
    public class SaveCourseResourceValidator : AbstractValidator<SaveCourseDTO>
    {
        public SaveCourseResourceValidator()
        {
            RuleFor(c => c.CourseName).NotEmpty();
            RuleFor(c => c.CategoryName).NotEmpty();
            RuleFor(c => c.Price).NotEmpty().GreaterThan(0);
            RuleFor(c => c.InstructorId).NotEmpty();
        }
    }
}
