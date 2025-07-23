using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace HRMApiApp.DTO
{
    public class EmployeeCreateDTOValidator : AbstractValidator<EmployeeCreateDTO>
    {
        public EmployeeCreateDTOValidator()
        {
            RuleFor(x=>x.EmployeeName).NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters");

            RuleFor(x => x.IdDepartment).NotEmpty()
                .WithMessage("Department id is required");
            RuleFor(x => x.IdSection).NotEmpty()
              .WithMessage("Section id is required");
        }
    }
}
