using EFCoreGenericRepositoryPattern.Models.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Validators
{
    public class SaveInstructorResourceValidator : AbstractValidator<SaveInstructorDTO>
    {
        public SaveInstructorResourceValidator()
        {
            RuleFor(i => i.FirstName).NotEmpty();
            RuleFor(i => i.LastName).NotEmpty();
            RuleFor(i => i.Email).NotEmpty().EmailAddress();
        }
    }
}
