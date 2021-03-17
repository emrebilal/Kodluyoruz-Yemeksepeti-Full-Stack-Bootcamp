using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithSwagger.Models;

namespace WebApiWithSwagger.Validators
{
    public class SaveCourseValidator : AbstractValidator<Course>
    {
        public SaveCourseValidator()
        {
            RuleFor(c => c.Id).Empty();
            RuleFor(c => c.CourseName).NotEmpty();
            RuleFor(c => c.CategoryName).NotEmpty();
            RuleFor(c => c.Price).NotEmpty().GreaterThan(0);
            RuleFor(c => c.InstructorName).NotEmpty();
        }
    }
}
