using ABInBev.Employees.Business.Interfaces;
using FluentValidation;

namespace ABInBev.Employees.Business.Models.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator(IEmployeeRepository employeeRepository, Guid? employeeId)
        {
            RuleFor(x => x.FirstName).NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);

            RuleFor(x => x.DocumentNumber).NotEmpty()
                .MustAsync(async (documentNumber, dummy) => !(await employeeRepository.IsDocumentNumberInUseAsync(documentNumber, employeeId)))
                    .WithMessage("This Document Number is already in use.");

            RuleFor(x => x.BirthDate).NotEmpty()
                .Must(x => x.Year <= DateTime.Now.AddYears(-18).Year)
                .WithMessage("The Employee must have at least 18 years old.");

            RuleFor(x => x.Phones).NotEmpty()
                .Must(x => x is not null && x.Count > 1).WithMessage("At least 2 phones are required.");
        }
    }
}
