using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithSwagger.Models;

namespace WebApiWithSwagger.Validators
{
    public class SaveInstructorValidator : AbstractValidator<Instructor>
    {
        public SaveInstructorValidator()
        {
            RuleFor(i => i.Id).Empty();
            RuleFor(i => i.FirstName).NotEmpty();
            RuleFor(i => i.LastName).NotEmpty();
            RuleFor(i => i.Email).NotEmpty().EmailAddress();
        }
    }
}
