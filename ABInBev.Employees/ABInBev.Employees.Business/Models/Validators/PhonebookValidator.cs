using FluentValidation;

namespace ABInBev.Employees.Business.Models.Validators
{
    public class PhonebookValidator : AbstractValidator<Phonebook>
    {
        public PhonebookValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.EmployeeId).NotEmpty();

            RuleFor(x => x.PhoneNumber).NotEmpty();

            RuleFor(x => x.Type).NotNull();
        }
    }
}
