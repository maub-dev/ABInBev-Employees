using ABInBev.Employees.Business.Interfaces;
using FluentValidation;

namespace ABInBev.Employees.Business.Models.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator(IEmployeeRepository employeeRepository)
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.FirstName).NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);

            RuleFor(x => x.DocumentNumber).NotEmpty()
                .MustAsync(async (documentNumber, dummy) => !(await employeeRepository.IsDocumentNumberInUseAsync(documentNumber)))
                    .WithMessage("This Document Number is already in use.");

            RuleFor(x => x.BirthDate).NotEmpty();

            RuleFor(x => x.Phones).NotEmpty()
                .Must(x => x is not null && x.Count > 1).WithMessage("At least 2 phones are required.");
        }
    }
}
